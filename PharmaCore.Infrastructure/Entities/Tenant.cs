namespace PharmaCore.Infrastructure.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Identifier { get; set; }
        public string? ConnectionString { get; set; }
    }
}
