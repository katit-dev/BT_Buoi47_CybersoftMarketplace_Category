using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ProductImage
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual Product Product { get; set; } = null!;
}
