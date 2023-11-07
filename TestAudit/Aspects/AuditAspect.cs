
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
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
    
    public override void OnExit(MethodExecutionArgs args)
    {
        SendAuditMessage(false, args);
    }

    public override void OnException(MethodExecutionArgs args)
    {
        SendAuditMessage(true, args);
    }

    private void SendAuditMessage(bool IsException, MethodExecutionArgs args)
    {

        var auditUser = AuditHelper.GetUserInfo(args);
        var auditEvent = AuditHelper.GetAuditEvent(_command);
        var auditData = AuditHelper.GetAuditData(args);
        var auditStatus = AuditHelper.GetAuditStatus(IsException, args);

        using (LogContext.PushProperty("audit_user", JsonConvert.SerializeObject(auditUser)))
        using (LogContext.PushProperty("audit_event", JsonConvert.SerializeObject(auditEvent)))
        using (LogContext.PushProperty("audit_data", JsonConvert.SerializeObject(auditData)))
        using (LogContext.PushProperty("audit_status", JsonConvert.SerializeObject(auditStatus)))
        {
            Log.Information("{audit_user} {audit_event} {audit_data} {audit_status}");
        }
    }

    
}