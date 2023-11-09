namespace TestAudit.Models;

public class AuditFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<long>? AuditUserIds { get; set; }
    public List<long>? AuditEventIds { get; set; }
    public List<long>? AuditStatusIds { get; set; }
}