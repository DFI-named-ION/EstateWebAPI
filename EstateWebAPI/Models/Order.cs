using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class Order
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string Address { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
