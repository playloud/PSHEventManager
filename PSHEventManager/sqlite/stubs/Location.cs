using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class Location
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public string? Country { get; set; }

    public string? Lng { get; set; }

    public string? Lat { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();
}
