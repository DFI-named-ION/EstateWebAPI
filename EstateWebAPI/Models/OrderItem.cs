using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class OrderItem
{
    public long Id { get; set; }

    public long OrderId { get; set; }

    public long EstateId { get; set; }

    public long Count { get; set; }

    public virtual Estate Estate { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
