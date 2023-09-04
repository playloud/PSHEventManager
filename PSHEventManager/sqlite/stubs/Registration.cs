using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class Registration
{
    public long Id { get; set; }

    public long EventId { get; set; }

    public long UserId { get; set; }

    public string Status { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
