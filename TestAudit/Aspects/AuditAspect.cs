
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using TestAudit.Controllers;
using TestAudit.Entities;
using TestAudit.Helpers.Audit;

namespace TestAudit.Aspects;

using PostSharp.Aspects;

[Serializable]
public class AuditAspect : OnMethodBoundaryAspect, IOnExceptionAspect
{
    private readonly AuditCommand _command;
    public AuditAspect(AuditCommand command)
    {
        _command = command;
    }
    public override void OnEntry(MethodExecutionArgs args)
    {
    }
    public override void OnExit(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;
        var auditUser = controller.User.Identity.Name;
        
        var auditEvent = new AuditEvent
        {
            Id = _command
        };
        
        Dictionary<string, object> auditData = new Dictionary<string, object>();
        if (args.Arguments.Count > 0)
        {
            auditData[args.Arguments.GetArgument(0).GetType().ToString()] = args.Arguments.GetArgument(0);
        }
        
        var auditStatus = new AuditStatus
        {
            Status = args.Arguments.Count > 1 ? args.Arguments.GetArgument(1).ToString() : "Success"
        };

        using (LogContext.PushProperty("audit_user", JsonConvert.SerializeObject(auditUser)))
        using (LogContext.PushProperty("audit_event", JsonConvert.SerializeObject(auditEvent)))
        using (LogContext.PushProperty("audit_data", JsonConvert.SerializeObject(auditData)))
        using (LogContext.PushProperty("audit_status", JsonConvert.SerializeObject(auditStatus)))
        {
            Log.Information("{audit_user} {audit_event} {audit_data} {audit_status}");
        }
    }

    public override void OnException(MethodExecutionArgs args)
    {
        var controller = args.Instance as ControllerBase;
        var auditUser = controller.User.FindFirst(ClaimTypes.NameIdentifier);
        
        var auditEvent = new AuditEvent
        {
            Id = _command
        };
        
        Dictionary<string, object> auditData = new Dictionary<string, object>();
        if (args.Arguments.Count > 0)
        {
            auditData[args.Arguments.GetArgument(0).GetType().Name] = args.Arguments.GetArgument(0);
        }
        
        var auditStatus = new AuditStatus
        {
            Status = args.Exception.Message
        };

        using (LogContext.PushProperty("audit_user", JsonConvert.SerializeObject(auditUser)))
        using (LogContext.PushProperty("audit_event", JsonConvert.SerializeObject(auditEvent)))
        using (LogContext.PushProperty("audit_data", JsonConvert.SerializeObject(auditData)))
        using (LogContext.PushProperty("audit_status", JsonConvert.SerializeObject(auditStatus)))
        {
            Log.Information("{audit_user} {audit_event} {audit_data} {audit_status}");
        }
    }
}