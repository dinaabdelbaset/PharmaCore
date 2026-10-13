using System;
using System.Collections.Generic;

namespace PharmaCore.Domain.Entities
{
    public sealed class Sale : BaseEntity
    {
        public Guid OrgId { get; set; }
        public Guid? BranchId { get; set; }
        public string InvoiceNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public Guid? CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerCode { get; set; }
        public string? CustomerPhone { get; set; }
        public string SaleType { get; set; } = string.Empty;       // نقدي/مسجل/آجل/رصيد/موظف/توصيل/تأمين
        public string MovementType { get; set; } = "بيع";
        public Guid? SellerEmployeeId { get; set; }
        public string? SellerName { get; set; }
        public string? SellerCode { get; set; }
        public bool UseAlternateSeller { get; set; }
        public decimal Subtotal { get; set; }
        public decimal DiscountPercent { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal TaxTotal { get; set; }
        public decimal Total { get; set; }
        public decimal PaidCash { get; set; }
        public decimal PaidVisa { get; set; }
        public decimal PaidFromBalance { get; set; }
        public decimal Remaining { get; set; }
        public string? Notes { get; set; }
        public Guid? InsuranceCompanyId { get; set; }
        public string? InsuranceCompanyName { get; set; }
        public string? InsuranceCode { get; set; }
        public string? ApprovalCode { get; set; }
        public decimal InsurancePercent { get; set; }
        public string? InsuranceMobile { get; set; }
        public string? DeliveryAddress { get; set; }
        public DateTime? ReminderDate { get; set; }
        public Guid? EmployeeCustomerId { get; set; }   // لو بيع لموظف
        public string? EmployeePayType { get; set; }     // نقدي/آجل/من الراتب
        public string Status { get; set; } = "Completed"; // Completed / Temp / Voided
        public Organization? Organization { get; set; }
        public Branch? Branch { get; set; }
        public Customer? Customer { get; set; }
        public Employee? SellerEmployee { get; set; }
        public ICollection<SaleLine>? Lines { get; set; }
    }
}
