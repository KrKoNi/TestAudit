using TestAudit.Models;

namespace TestAudit.Services;

public class WorkspaceService : IWorkspaceService
{
    private readonly Random _random = new();

    public async Task<long?> CreateWorkspace(Workspace workspace)
    {
        return _random.NextInt64();
    }
}