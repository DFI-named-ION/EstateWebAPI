using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class EstateLike
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public long EstateId { get; set; }

    public virtual Estate Estate { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
