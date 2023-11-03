namespace TestAudit.Models;

public class User
{
    public string Login { get; set; }
    public long? UserId { get; set; }
    public string? Surname { get; set; }
    public string? Name { get; set; }
    public string? Patronymic { get; set; }
    public string? UserRole { get; set; }
}