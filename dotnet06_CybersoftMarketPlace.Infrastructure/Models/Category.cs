using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public int ShopId { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual Shop Shop { get; set; } = null!;
}
