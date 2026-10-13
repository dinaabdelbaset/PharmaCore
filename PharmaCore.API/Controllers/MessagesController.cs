using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PharmaCore.Infrastructure.Data;
using PharmaCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PharmaCore.API.Controllers
{
    [ApiController, Route("api/messages"), Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly AppDbContext _db;

        public MessagesController(AppDbContext db)
        {
            _db = db;
            
            // Auto-create database table if not exists for safe production hot-deployment
            try
            {
                _db.Database.ExecuteSqlRaw(@"
                    CREATE TABLE IF NOT EXISTS ""PharmaCoreMessages"" (
                        ""Id"" uuid NOT NULL PRIMARY KEY,
                        ""OrgId"" uuid NOT NULL,
                        ""BranchId"" uuid NULL,
                        ""SenderId"" uuid NOT NULL,
                        ""SenderName"" text NOT NULL,
                        ""SenderRole"" text NOT NULL,
                        ""TargetId"" text NOT NULL,
                        ""TargetType"" text NOT NULL,
                        ""Content"" text NOT NULL,
                        ""Timestamp"" timestamp with time zone NOT NULL,
                        ""Read"" boolean NOT NULL DEFAULT FALSE
                    );
                ");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Auto-creating PharmaCoreMessages table warning: " + ex.Message);
            }
        }

        private Guid OrgId
        {
            get
            {
                var claim = User.FindFirst("org_id");
                return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
            }
        }

        private Guid EmployeeId
        {
            get
            {
                var claim = User.FindFirst("employee_id") ?? User.FindFirst("user_id");
                return claim != null && Guid.TryParse(claim.Value, out var id) ? id : Guid.Empty;
            }
        }

        private Guid? BranchId
        {
            get
            {
                var claim = User.FindFirst("branch_id");
                return claim != null && Guid.TryParse(claim.Value, out var id) ? id : null;
            }
        }

        private string EmployeeName => User.FindFirst(ClaimTypes.Name)?.Value ?? "User";
        private string EmployeeRole => User.FindFirst(ClaimTypes.Role)?.Value ?? "Staff";

        // DTO models
        public record MessageDto(
            Guid Id, Guid OrgId, Guid? BranchId, Guid SenderId, 
            string SenderName, string SenderRole, string TargetId, 
            string TargetType, string Content, DateTime Timestamp, bool Read);

        public record SendMessageReq(string TargetId, string TargetType, string Content);

        // GET: api/messages/conversations
        [HttpGet("conversations")]
        public async Task<IActionResult> GetConversations()
        {
            var orgId = OrgId;
            var empIdStr = EmployeeId.ToString();
            var branchIdStr = BranchId?.ToString() ?? string.Empty;

            // Fetch all messages related to this user/organization
            var messages = await _db.PharmaCoreMessages
                .Where(m => m.OrgId == orgId &&
                            (m.TargetType == "all" ||
                             (m.TargetType == "branch" && m.TargetId == branchIdStr) ||
                             (m.TargetType == "employee" && (m.TargetId == empIdStr || m.SenderId == EmployeeId))))
                .OrderByDescending(m => m.Timestamp)
                .ToListAsync();

            // Group messages into conversations
            var conversationGroups = new Dictionary<string, List<PharmaCoreMessage>>();

            foreach (var m in messages)
            {
                string key;
                if (m.TargetType == "all")
                {
                    key = "all";
                }
                else if (m.TargetType == "branch")
                {
                    key = $"branch-{m.TargetId}";
                }
                else // employee direct message
                {
                    var otherParty = m.SenderId == EmployeeId ? m.TargetId : m.SenderId.ToString();
                    key = $"employee-{otherParty}";
                }

                if (!conversationGroups.ContainsKey(key))
                {
                    conversationGroups[key] = new List<PharmaCoreMessage>();
                }
                conversationGroups[key].Add(m);
            }

            var conversationsList = new List<object>();

            foreach (var kv in conversationGroups)
            {
                var mostRecent = kv.Value.First();
                var unreadCount = kv.Value.Count(m => !m.Read && m.SenderId != EmployeeId);

                string type = mostRecent.TargetType;
                string targetId;
                string targetName;
                string targetTypeLabel;

                if (type == "all")
                {
                    targetId = "all";
                    targetName = "إعلان عام للمنظومة (Global Notice)";
                    targetTypeLabel = "System Notice";
                }
                else if (type == "branch")
                {
                    targetId = mostRecent.TargetId;
                    var branch = await _db.Branches.FirstOrDefaultAsync(b => b.Id == Guid.Parse(targetId));
                    targetName = branch != null ? $"فرع: {branch.Name}" : "فرع غير معروف";
                    targetTypeLabel = "Branch Notice";
                }
                else // employee direct message
                {
                    var otherPartyId = mostRecent.SenderId == EmployeeId ? Guid.Parse(mostRecent.TargetId) : mostRecent.SenderId;
                    var emp = await _db.Employees.FirstOrDefaultAsync(e => e.Id == otherPartyId);
                    targetId = otherPartyId.ToString();
                    targetName = emp != null ? emp.FullName : mostRecent.SenderId == EmployeeId ? "مستقبل الرسالة" : mostRecent.SenderName;
                    targetTypeLabel = emp != null ? emp.Role : "Staff";
                }

                conversationsList.Add(new
                {
                    id = kv.Key,
                    type = type,
                    targetId = targetId,
                    targetName = targetName,
                    targetType = targetTypeLabel,
                    unreadCount = unreadCount,
                    lastMessage = new
                    {
                        id = mostRecent.Id,
                        senderId = mostRecent.SenderId,
                        senderName = mostRecent.SenderName,
                        senderRole = mostRecent.SenderRole,
                        content = mostRecent.Content,
                        timestamp = mostRecent.Timestamp,
                        read = mostRecent.Read
                    }
                });
            }

            return Ok(conversationsList);
        }

        // GET: api/messages/conversations/{targetId}
        [HttpGet("conversations/{targetId}")]
        public async Task<IActionResult> GetConversationMessages(string targetId, [FromQuery] string type)
        {
            var orgId = OrgId;
            var empIdStr = EmployeeId.ToString();
            var branchIdStr = BranchId?.ToString() ?? string.Empty;

            List<PharmaCoreMessage> history;

            if (type == "all")
            {
                history = await _db.PharmaCoreMessages
                    .Where(m => m.OrgId == orgId && m.TargetType == "all")
                    .OrderBy(m => m.Timestamp)
                    .ToListAsync();
            }
            else if (type == "branch")
            {
                history = await _db.PharmaCoreMessages
                    .Where(m => m.OrgId == orgId && m.TargetType == "branch" && m.TargetId == targetId)
                    .OrderBy(m => m.Timestamp)
                    .ToListAsync();
            }
            else // employee
            {
                var otherEmpId = Guid.Parse(targetId);
                history = await _db.PharmaCoreMessages
                    .Where(m => m.OrgId == orgId && m.TargetType == "employee" &&
                                ((m.SenderId == EmployeeId && m.TargetId == targetId) ||
                                 (m.SenderId == otherEmpId && m.TargetId == empIdStr)))
                    .OrderBy(m => m.Timestamp)
                    .ToListAsync();
            }

            // Mark received messages as read
            var unread = history.Where(m => !m.Read && m.SenderId != EmployeeId).ToList();
            if (unread.Any())
            {
                foreach (var m in unread) m.Read = true;
                await _db.SaveChangesAsync();
            }

            var result = history.Select(m => new MessageDto(
                m.Id, m.OrgId, m.BranchId, m.SenderId,
                m.SenderName, m.SenderRole, m.TargetId,
                m.TargetType, m.Content, m.Timestamp, m.Read
            ));

            return Ok(result);
        }

        // POST: api/messages
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] SendMessageReq req)
        {
            var orgId = OrgId;
            if (orgId == Guid.Empty) return Unauthorized();

            var message = new PharmaCoreMessage
            {
                Id = Guid.NewGuid(),
                OrgId = orgId,
                BranchId = BranchId,
                SenderId = EmployeeId,
                SenderName = EmployeeName,
                SenderRole = EmployeeRole,
                TargetId = req.TargetId,
                TargetType = req.TargetType,
                Content = req.Content,
                Timestamp = DateTime.UtcNow,
                Read = false
            };

            _db.PharmaCoreMessages.Add(message);
            await _db.SaveChangesAsync();

            return Ok(new MessageDto(
                message.Id, message.OrgId, message.BranchId, message.SenderId,
                message.SenderName, message.SenderRole, message.TargetId,
                message.TargetType, message.Content, message.Timestamp, message.Read
            ));
        }

        // PUT: api/messages/{id}/read
        [HttpPut("{id:guid}/read")]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var message = await _db.PharmaCoreMessages.FindAsync(id);
            if (message == null) return NotFound();

            message.Read = true;
            await _db.SaveChangesAsync();
            return Ok();
        }
    }
}
