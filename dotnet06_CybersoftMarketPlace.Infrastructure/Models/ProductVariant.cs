using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class ProductVariant
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? VariantName { get; set; }

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Product Product { get; set; } = null!;
}
