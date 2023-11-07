using Npgsql;
using TestAudit.Entities;

namespace TestAudit.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly string _connectionString;
    private readonly IConfiguration _configuration;

    public AuditRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("auditDb");
    }
    public IEnumerable<AuditRecord> GetAllAuditRecords()
    {
        List<AuditRecord> auditRecords = new List<AuditRecord>();

        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string query = "SELECT audit_data, audit_event, audit_status, audit_user, datetime FROM public.audit_event_log";

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                connection.Open();

                using (NpgsqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AuditRecord record = new AuditRecord
                        {
                            AuditData = reader.GetString(0),
                            AuditEvent = reader.GetString(1),
                            AuditStatus = reader.GetString(2),
                            User = reader.GetString(3),
                            DateTime = reader.GetDateTime(4)
                        };

                        auditRecords.Add(record);
                    }
                }
            }
        }

        return auditRecords;
    }
}