using System;

namespace PharmaCore.Domain.Entities
{
    public class PharmaCoreMessage
    {
        public Guid Id { get; set; }
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid SenderId { get; set; }
        public string SenderName { get; set; } = string.Empty;
        public string SenderRole { get; set; } = string.Empty;
        public string TargetId { get; set; } = string.Empty;
        public string TargetType { get; set; } = string.Empty; // "employee", "branch", "all"
        public string Content { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public bool Read { get; set; }
    }
}
