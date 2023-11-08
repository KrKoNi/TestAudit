namespace TestAudit.Entities;

public class AuditFilter
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<long>? AuditUsers { get; set; }
    public List<long>? AuditEvents { get; set; }
    public List<long>? AuditStatuses { get; set; }
}