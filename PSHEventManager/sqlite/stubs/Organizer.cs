using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class Organizer
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
