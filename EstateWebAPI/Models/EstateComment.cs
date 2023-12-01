using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class EstateComment
{
    public long Id { get; set; }

    public string Text { get; set; } = null!;

    public long UserId { get; set; }

    public long EstateId { get; set; }

    public virtual Estate Estate { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
