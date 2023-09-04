using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class DjangoMigration
{
    public long Id { get; set; }

    public string App { get; set; } = null!;

    public string Name { get; set; } = null!;

    public byte[] Applied { get; set; } = null!;
}
