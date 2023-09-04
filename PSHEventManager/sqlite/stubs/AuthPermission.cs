using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class AuthPermission
{
    public long Id { get; set; }

    public long ContentTypeId { get; set; }

    public string Codename { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<AuthGroupPermission> AuthGroupPermissions { get; set; } = new List<AuthGroupPermission>();

    public virtual ICollection<AuthUserUserPermission> AuthUserUserPermissions { get; set; } = new List<AuthUserUserPermission>();

    public virtual DjangoContentType ContentType { get; set; } = null!;
}
