using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class CashRegisterEntry : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public Guid? SaleId { get; set; }
        public decimal Amount { get; set; }
        public string EntryType { get; set; } = string.Empty;  // مبيعات/مبيعات آجل/سحب من راتب/فيزا
        public string? Description { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public bool IsBank { get; set; }   // true = بنك/فيزا, false = كاش
        public DateTime Date { get; set; }
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public Sale? Sale { get; set; }
    }
}
