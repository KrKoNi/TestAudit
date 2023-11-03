namespace TestAudit.Models;

public class Workspace
{
    public long WorkspaceId { get; set; }
    public string WorkspaceName { get; set; }
    public Group Group { get; set; }
    public GeoLayer GeoLayer { get; set; }
}