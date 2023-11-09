using TestAudit.Entities;
using TestAudit.Models;

namespace TestAudit.Repositories;

public interface IAuditRepository
{
    IEnumerable<AuditRecord> GetAllAuditRecords();
    IEnumerable<AuditRecord> GetFilteredAuditRecords(AuditFilter auditFilter);
}