using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAudit.Aspects;
using TestAudit.Helpers.Audit;
using TestAudit.Models;
using TestAudit.Services;

namespace TestAudit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkspaceController : ControllerBase
{
    private readonly IWorkspaceService _workspaceService;

    public WorkspaceController(IWorkspaceService workspaceService)
    {
        _workspaceService = workspaceService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AuditAspect(command: AuditCommand.CreateWorkspace)]
    public async Task<IActionResult> Post([FromBody] Workspace workspace)
    {
        var response = await _workspaceService.CreateWorkspace(workspace);
        if (response == null)
        {
            return new BadRequestResult();
        }
        return new OkResult();
    }
    
}