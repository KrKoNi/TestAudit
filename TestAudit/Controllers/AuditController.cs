﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAudit.Services;

namespace TestAudit.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditController : ControllerBase
{
    private readonly IAuditService _auditService;

    public AuditController(IAuditService auditService)
    {
        _auditService = auditService;
    }
    
    [AllowAnonymous]
    [HttpPost("GetAllAuditRecords")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAllAuditRecords()
    {
        var response = _auditService.GetAllAuditRecords();
        if (response == null)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}