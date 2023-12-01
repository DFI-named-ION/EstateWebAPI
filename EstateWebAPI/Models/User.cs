using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EstateWebAPI.Models;

public partial class User
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public long RoleId { get; set; }

    public virtual ICollection<EstateComment> EstateComments { get; set; } = new List<EstateComment>();
        
    public virtual ICollection<EstateLike> EstateLikes { get; set; } = new List<EstateLike>();

    public virtual ICollection<Estate> Estates { get; set; } = new List<Estate>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;
}
