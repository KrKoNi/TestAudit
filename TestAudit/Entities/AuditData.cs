namespace TestAudit.Entities;

public class AuditData
{
    public string? RequestMethod { get; set; }
    public string? EndpointUrl { get; set; }
    public Dictionary<string, object>? AuditObject { get; set; }
}
