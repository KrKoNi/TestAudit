﻿using Npgsql;
using TestAudit.Entities;

namespace TestAudit.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly string _connectionString;

    public AuditRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("auditDb");
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

    public IEnumerable<AuditRecord> GetFilteredAuditRecords(AuditFilter auditFilter)
    {
        using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
        {
            string query = "SELECT audit_data, audit_event, audit_status, audit_user, datetime FROM public.audit_event_log";
            List<AuditRecord> auditRecords = new List<AuditRecord>();
            
            List<string> conditions = new List<string>();
            if (auditFilter.AuditUsers is not null && auditFilter.AuditUsers.Count != 0)
            {
                conditions.Add("CAST(audit_user->>'Id' AS bigint) = ANY(@AuditUsers)");
            }
            if (auditFilter.AuditEvents is not null && auditFilter.AuditEvents.Count != 0)
            {
                conditions.Add("CAST(audit_event->>'Id' AS bigint) = ANY(@AuditEvents)");
            }
            if (auditFilter.AuditStatuses is not null && auditFilter.AuditStatuses.Count != 0)
            {
                conditions.Add("CAST(audit_status->>'Code' AS bigint) = ANY(@AuditStatuses)");
            }
            if (auditFilter.StartDate.HasValue)
            {
                conditions.Add("datetime >= (@StartDate)");
            }
            if (auditFilter.EndDate.HasValue)
            {
                conditions.Add("datetime <= (@EndDate)");
            }

            if (conditions.Count > 0)
            {
                query += " WHERE " + string.Join(" AND ", conditions);
            }

            using (NpgsqlCommand command = new NpgsqlCommand(query, connection))
            {
                if (auditFilter.AuditUsers is not null && auditFilter.AuditUsers.Count != 0)
                {
                    command.Parameters.AddWithValue("AuditUsers", auditFilter.AuditUsers);
                }
                if (auditFilter.AuditEvents is not null && auditFilter.AuditEvents.Count != 0)
                {
                    command.Parameters.AddWithValue("AuditEvents", auditFilter.AuditEvents);
                }
                if (auditFilter.AuditStatuses is not null && auditFilter.AuditStatuses.Count != 0)
                {
                    command.Parameters.AddWithValue("AuditStatuses", auditFilter.AuditStatuses);
                }
                if (auditFilter.StartDate.HasValue)
                {
                    command.Parameters.AddWithValue("StartDate", auditFilter.StartDate);
                }
                if (auditFilter.EndDate.HasValue)
                {
                    command.Parameters.AddWithValue("EndDate", auditFilter.EndDate);
                }

                connection.Open();
                Console.WriteLine(command.CommandText);
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

            return auditRecords;
        }
    }
}