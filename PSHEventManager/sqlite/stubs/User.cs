using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class User
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string Role { get; set; } = null!;

    public byte[]? CreatedAt { get; set; }

    public byte[]? UpdatedAt { get; set; }

    public virtual ICollection<Registration> Registrations { get; set; } = new List<Registration>();
}
