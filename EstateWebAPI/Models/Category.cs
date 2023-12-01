using System;
using System.Collections.Generic;

namespace EstateWebAPI.Models;

public partial class Category
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();
}
