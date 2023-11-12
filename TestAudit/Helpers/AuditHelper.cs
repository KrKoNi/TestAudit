using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PostSharp.Aspects;
using TestAudit.Entities;

namespace TestAudit.Helpers;

public static class AuditHelper
{
    public static AuditUser GetUserInfo(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;

        var userIdString = controller?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!long.TryParse(userIdString, out var userId))
        {
            userId = -1;
        }

        var login = controller?.User.FindFirstValue("login") ?? "Anonymous";
        var surname = controller?.User.FindFirstValue(ClaimTypes.Surname);
        var name = controller?.User.FindFirstValue(ClaimTypes.Name);
        var patronymic = controller?.User.FindFirstValue("patronymic");
        var permissions = controller?.User.FindAll("permission").Select(x => x.Value).ToList();
        var role = controller?.User.FindFirstValue("role");
        var remoteIpAddress = controller?.HttpContext.Connection.RemoteIpAddress?.ToString();
        
        var auditUser = new AuditUser
        {
            Id = userId,
            Login = login,
            Surname = surname,
            Name = name,
            Patronymic = patronymic,
            Permissions = permissions,
            Role = role,
            Ip = remoteIpAddress
        };
        return auditUser;
    }

    public static AuditEvent GetAuditEvent(AuditCommand command)
    {
        var auditEvent = new AuditEvent
        {
            Id = command
        };
        return auditEvent;
    }

    public static AuditData GetAuditData(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;
        
        Dictionary<string, object> auditObject = new Dictionary<string, object>();
        foreach (var argument in args.Arguments)
        {
            auditObject[argument.GetType().Name] = argument;
        }
        
        var auditData = new AuditData
        {
            RequestMethod = controller?.Request.Method,
            EndpointUrl = controller?.Request.Path,
            AuditObject = auditObject
        };
        return auditData;
    }

    public static AuditStatus GetAuditStatus(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;
        
        var auditStatus = new AuditStatus
        {
            Code = controller?.Response.StatusCode
        };
        return auditStatus;
    }
}