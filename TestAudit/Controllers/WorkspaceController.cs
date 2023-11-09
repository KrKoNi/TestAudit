using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAudit.Aspects;
using TestAudit.Entities;
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
    [HttpPost("CreateWorkspace")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AuditAspect(command: AuditCommand.CreateWorkspace)]
    public async Task<IActionResult> CreateWorkspace([FromBody] Workspace workspace)
    {
        var response = await _workspaceService.CreateWorkspace(workspace);
        if (response == null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
    
    [AllowAnonymous]
    [HttpGet("GetWorkspace")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AuditAspect(command: AuditCommand.CreateWorkspace)]
    public async Task<IActionResult> GetWorkspace([FromQuery] Workspace workspace)
    {
        return Ok(workspace);
    }
    
}