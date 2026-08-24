using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class VGetAllProductsDetailV
{
    public int Id { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductVariantId { get; set; }

    public string? ProductVariantName { get; set; }

    public decimal Price { get; set; }

    public string TenShop { get; set; } = null!;
}
