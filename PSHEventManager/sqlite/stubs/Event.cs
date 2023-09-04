using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class Event
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public byte[] StartDate { get; set; } = null!;

    public byte[] EndDate { get; set; } = null!;

    public long LocationId { get; set; }

    public long OrganizerId { get; set; }

    public long Capacity { get; set; }

    public byte[]? CreatedAt { get; set; }

    public byte[]? UpdatedAt { get; set; }

    public virtual Location Location { get; set; } = null!;

    public virtual Organizer Organizer { get; set; } = null!;

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
    public List<User> RegisteredUsers { get; set; }
}
