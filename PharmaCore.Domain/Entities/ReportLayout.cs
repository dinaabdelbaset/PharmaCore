using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class ReportLayout : BaseEntity
    {
        public Guid OrgId { get; set; }
        public string ReportName { get; set; } = string.Empty;
        public string LayoutXml { get; set; } = string.Empty;
        public Organization? Organization { get; set; }
    }
}
