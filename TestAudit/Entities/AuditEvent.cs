﻿using Microsoft.OpenApi.Extensions;

namespace TestAudit.Entities;

public class AuditEvent
{
    private AuditCommand _id;

    public AuditCommand Id
    {
        get => _id;
        set
        {
            _id = value;
            Name = _id.GetDisplayName();
        }
    }

    public string Name { get; private set; }
}