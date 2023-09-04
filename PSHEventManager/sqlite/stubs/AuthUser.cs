using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class AuthUser
{
    public long Id { get; set; }

    public string Password { get; set; } = null!;

    public byte[]? LastLogin { get; set; }

    public byte[] IsSuperuser { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public byte[] IsStaff { get; set; } = null!;

    public byte[] IsActive { get; set; } = null!;

    public byte[] DateJoined { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public virtual ICollection<AuthUserGroup> AuthUserGroups { get; set; } = new List<AuthUserGroup>();

    public virtual ICollection<AuthUserUserPermission> AuthUserUserPermissions { get; set; } = new List<AuthUserUserPermission>();

    public virtual ICollection<DjangoAdminLog> DjangoAdminLogs { get; set; } = new List<DjangoAdminLog>();
}
