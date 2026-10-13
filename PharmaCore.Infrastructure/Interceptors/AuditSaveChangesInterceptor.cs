using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using PharmaCore.Domain.Entities;

namespace PharmaCore.Infrastructure.Interceptors
{
    public class AuditSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuditSaveChangesInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            if (eventData.Context != null)
            {
                ApplyAuditAndSoftDelete(eventData.Context);
            }
            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if (eventData.Context != null)
            {
                ApplyAuditAndSoftDelete(eventData.Context);
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void ApplyAuditAndSoftDelete(DbContext context)
        {
            var now = DateTime.UtcNow;

            // 1. Resolve HTTP Context user details (if available)
            var http = _httpContextAccessor?.HttpContext;
            
            var empIdStr = http?.User?.FindFirstValue("employee_id");
            Guid? employeeId = Guid.TryParse(empIdStr, out var empGuid) ? empGuid : null;
            var employeeCode = http?.User?.FindFirstValue("employee_code");
            var employeeName = http?.User?.FindFirstValue(ClaimTypes.Name);
            
            var orgIdStr = http?.User?.FindFirstValue("org_id");
            Guid orgId = Guid.TryParse(orgIdStr, out var orgGuid) ? orgGuid : Guid.Empty;
            
            var branchIdStr = http?.User?.FindFirstValue("branch_id");
            Guid? branchId = Guid.TryParse(branchIdStr, out var branchGuid) ? branchGuid : null;

            // 2. Perform Soft-Delete Transformation (EntityState.Deleted -> Modified + IsActive = false)
            var deletedEntries = context.ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Entity is BaseEntity)
                .ToList();

            foreach (var entry in deletedEntries)
            {
                entry.State = EntityState.Modified;
                var baseEntity = (BaseEntity)entry.Entity;
                baseEntity.IsActive = false;
                baseEntity.UpdatedAt = now;
            }

            // 3. Process Audit Tracking for Added or Modified entries
            var trackedEntries = context.ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified))
                .ToList();

            var auditEvents = new List<AuditEvent>();

            foreach (var entry in trackedEntries)
            {
                // Skip auditing audit tables to avoid infinite loop
                if (entry.Entity is AuditEvent || entry.Entity is AuditDataChange || entry.Entity is AuditSession)
                {
                    continue;
                }

                var baseEntity = (BaseEntity)entry.Entity;
                var state = entry.State;

                // Resolve OrgId and BranchId from entity properties if not found in HTTP Context
                var eventOrgId = orgId;
                if (eventOrgId == Guid.Empty)
                {
                    var p = entry.Entity.GetType().GetProperty("OrgId");
                    if (p != null)
                    {
                        var val = p.GetValue(entry.Entity);
                        if (val is Guid g) eventOrgId = g;
                    }
                }

                var eventBranchId = branchId;
                if (eventBranchId == null)
                {
                    var p = entry.Entity.GetType().GetProperty("BranchId");
                    if (p != null)
                    {
                        var val = p.GetValue(entry.Entity);
                        if (val is Guid g) eventBranchId = g;
                    }
                }

                var eventType = state.ToString();
                var targetTable = entry.Entity.GetType().Name;
                
                // If it is added, the ID might not be generated yet if using database-generated GUIDs, 
                // but since we are using client/code-assigned GUIDs or base entities, we extract it.
                var targetId = baseEntity.Id.ToString();

                var auditEvent = new AuditEvent
                {
                    Id = Guid.NewGuid(),
                    OrgId = eventOrgId,
                    BranchId = eventBranchId,
                    EmployeeId = employeeId,
                    EmployeeCode = employeeCode,
                    EmployeeName = employeeName,
                    EventType = eventType,
                    Category = "DataChange",
                    TargetTable = targetTable,
                    TargetId = targetId,
                    Description = $"Data change: {eventType} on {targetTable} (ID: {targetId})",
                    Severity = 0,
                    CreatedAt = now,
                    DataChanges = new List<AuditDataChange>()
                };

                foreach (var prop in entry.Properties)
                {
                    // Skip primary key fields and auditing control fields
                    if (prop.Metadata.IsPrimaryKey() || prop.Metadata.Name == "CreatedAt" || prop.Metadata.Name == "UpdatedAt")
                    {
                        continue;
                    }

                    var fieldName = prop.Metadata.Name;
                    var isModified = prop.IsModified;
                    string? oldValue = null;
                    string? newValue = prop.CurrentValue?.ToString();

                    if (state == EntityState.Modified)
                    {
                        oldValue = prop.OriginalValue?.ToString();
                        
                        // Skip if the property did not change value
                        if (!isModified || oldValue == newValue)
                        {
                            continue;
                        }
                    }

                    auditEvent.DataChanges.Add(new AuditDataChange
                    {
                        Id = Guid.NewGuid(),
                        AuditEventId = auditEvent.Id,
                        FieldName = fieldName,
                        OldValue = oldValue,
                        NewValue = newValue
                    });
                }

                // Add to list only if there are recorded changes (or if it is a new entity)
                if (state == EntityState.Added || auditEvent.DataChanges.Count > 0)
                {
                    auditEvents.Add(auditEvent);
                }
            }

            if (auditEvents.Count > 0)
            {
                context.Set<AuditEvent>().AddRange(auditEvents);
            }
        }
    }
}
