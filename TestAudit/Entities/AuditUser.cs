
namespace TestAudit.Entities;

public class AuditUser
{
    public long Id { get; set; }
    public string Login { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public string? Ip { get; set; }
    public List<string>? Permissions { get; set; }
    public string? Role { get; set; }
    
}
