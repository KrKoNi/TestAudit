using TestAudit.Entities;
using TestAudit.Repositories;

namespace TestAudit.Services;

public class AuditService : IAuditService
{

    private readonly IAuditRepository _auditRepository;

    public AuditService(IAuditRepository auditRepository)
    {
        _auditRepository = auditRepository;
    }

    public IEnumerable<AuditRecord> GetAllAuditRecords()
    {
        return _auditRepository.GetAllAuditRecords();
    }

    public IEnumerable<AuditRecord> GetFilteredAuditRecords(AuditFilter auditFilter)
    {
        return _auditRepository.GetFilteredAuditRecords(auditFilter);
    }
}