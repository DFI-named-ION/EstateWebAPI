using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class Estate
{
    public long Id { get; set; }

    public string Image { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public long OwnerId { get; set; }

    public decimal Price { get; set; }

    public long FloorCount { get; set; }

    public long RoomCount { get; set; }

    public long CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<EstateComment> EstateComments { get; set; } = new List<EstateComment>();

    public virtual ICollection<EstateLike> EstateLikes { get; set; } = new List<EstateLike>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User Owner { get; set; } = null!;
}
