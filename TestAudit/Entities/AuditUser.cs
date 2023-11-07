using System.Security.Claims;

namespace TestAudit.Entities;

public class AuditUser
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public List<string> Permissions { get; set; }
}