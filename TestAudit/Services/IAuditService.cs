﻿using TestAudit.Entities;

namespace TestAudit.Services;

public interface IAuditService
{
    IEnumerable<AuditRecord> GetAllAuditRecords();
    IEnumerable<AuditRecord> GetFilteredAuditRecords(AuditFilter auditFilter);
}