
namespace TestAudit.Entities;

public class AuditUser
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public List<string>? Permissions { get; set; }
}
