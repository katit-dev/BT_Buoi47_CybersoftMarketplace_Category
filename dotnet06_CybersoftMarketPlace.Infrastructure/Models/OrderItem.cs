using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int VariantId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductVariant Variant { get; set; } = null!;
}
