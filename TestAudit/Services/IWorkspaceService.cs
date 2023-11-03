using TestAudit.Models;

namespace TestAudit.Services;

public interface IWorkspaceService
{
    Task<long?> CreateWorkspace(Workspace workspace);
}