using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class AuditDataChange
    {
        public Guid Id { get; set; }
        public Guid AuditEventId { get; set; }
        public string FieldName { get; set; } = string.Empty;
        public string? FieldLabel { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public AuditEvent? AuditEvent { get; set; }
    }
}
