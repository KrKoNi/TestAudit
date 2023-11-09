
using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using TestAudit.Entities;
using TestAudit.Helpers;

namespace TestAudit.Aspects;

using PostSharp.Aspects;

[Serializable]
public class AuditAspect : OnMethodBoundaryAspect
{
    private readonly AuditCommand _command;

    public AuditAspect(AuditCommand command)
    {
        _command = command;
    }
    
    public override void OnExit(MethodExecutionArgs args)
    {
        var auditUser = AuditHelper.GetUserInfo(args);
        var auditEvent = AuditHelper.GetAuditEvent(_command);
        var auditData = AuditHelper.GetAuditData(args);
        var auditStatus = AuditHelper.GetAuditStatus(args);
        AuditLogger.SendAuditMessage(auditUser, auditEvent, auditData, auditStatus);
    }

    

    
}