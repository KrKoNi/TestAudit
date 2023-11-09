namespace TestAudit.Models;

public class AuditRecord
{
    public DateTime DateTime { get; set; }
    public string User { get; set; }
    public string AuditEvent { get; set; }
    public string AuditData { get; set; }
    public string AuditStatus { get; set; }
}