using System;
using System.Collections.Generic;

namespace PSHEventManager.sqlite.stubs;

public partial class DjangoSession
{
    public string SessionKey { get; set; } = null!;

    public string SessionData { get; set; } = null!;

    public byte[] ExpireDate { get; set; } = null!;
}
