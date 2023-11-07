using TestAudit.Entities;

namespace TestAudit.Repositories;

public interface IAuditRepository
{
    IEnumerable<AuditRecord> GetAllAuditRecords();
}