using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Product
{
    public int Id { get; set; }

    public int ShopId { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public string? Image { get; set; }

    public decimal DisplayPrice { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual Shop Shop { get; set; } = null!;
}
