using Newtonsoft.Json;
using Serilog;
using Serilog.Context;
using TestAudit.Entities;

namespace TestAudit.Helpers;

public static class AuditLogger
{
    public static void SendAuditMessage(AuditUser auditUser, AuditEvent auditEvent, AuditData auditData, AuditStatus auditStatus)
    {
        using (LogContext.PushProperty("audit_user", JsonConvert.SerializeObject(auditUser)))
        using (LogContext.PushProperty("audit_event", JsonConvert.SerializeObject(auditEvent)))
        using (LogContext.PushProperty("audit_data", JsonConvert.SerializeObject(auditData)))
        using (LogContext.PushProperty("audit_status", JsonConvert.SerializeObject(auditStatus)))
        {
            Log.Information("{audit_user} {audit_event} {audit_data} {audit_status}");
        }
    }
}