using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PostSharp.Aspects;
using TestAudit.Entities;

namespace TestAudit.Helpers.Audit;

public class AuditHelper
{
    public static AuditUser GetUserInfo(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;
        
        var userId = controller?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var successParseUserId = long.TryParse(userId, out var longUserId);
        var parsedValue = successParseUserId ? longUserId : -1;
        
        var userName = controller?.User.FindFirstValue(ClaimTypes.Name);
        
        var permissions = controller?.User.FindAll("permission");
        
        var auditUser = new AuditUser
        {
            Id = parsedValue,
            Name = userName,
            Permissions = permissions.Select(x => x.Value).ToList()
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

    public static Dictionary<string, object> GetAuditData(MethodExecutionArgs args)
    {
        Dictionary<string, object> auditData = new Dictionary<string, object>();
        if (args.Arguments.Count > 0)
        {
            auditData[args.Arguments.GetArgument(0).GetType().Name] = args.Arguments.GetArgument(0);
        }

        return auditData;
    }

    public static AuditStatus GetAuditStatus(bool IsException, MethodExecutionArgs args)
    {
        var auditStatus = new AuditStatus
        {
            Code = IsException ? StatusCode.Exception : (args.Arguments.Count > 1 ? (StatusCode)args.Arguments.GetArgument(1) : StatusCode.Success)
        };
        return auditStatus;
    }
}