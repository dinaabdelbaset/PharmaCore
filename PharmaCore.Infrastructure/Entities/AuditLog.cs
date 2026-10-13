using System;

namespace PharmaCore.Infrastructure.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string EntityName { get; set; } = string.Empty;
        public string EntityId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty; // Create, Update, Delete
        public string? UserId { get; set; }
        public string? TenantId { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Changes { get; set; } // JSON blob with before/after
    }
}
