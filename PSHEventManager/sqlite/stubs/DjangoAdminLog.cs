using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class DjangoAdminLog
{
    public long Id { get; set; }

    public string? ObjectId { get; set; }

    public string ObjectRepr { get; set; } = null!;

    public long ActionFlag { get; set; }

    public string ChangeMessage { get; set; } = null!;

    public long? ContentTypeId { get; set; }

    public long UserId { get; set; }

    public byte[] ActionTime { get; set; } = null!;

    public virtual DjangoContentType? ContentType { get; set; }

    public virtual AuthUser User { get; set; } = null!;
}
