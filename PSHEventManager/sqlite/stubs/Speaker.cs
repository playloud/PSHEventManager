using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class Speaker
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Bio { get; set; }

    public string? Photo { get; set; }
}
