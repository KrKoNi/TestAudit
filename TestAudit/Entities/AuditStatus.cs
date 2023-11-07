using TestAudit.Helpers;

namespace TestAudit.Entities;

public class AuditStatus
{
    public StatusCode? Code { get; set; }
    public string? Message { get; set; }
}