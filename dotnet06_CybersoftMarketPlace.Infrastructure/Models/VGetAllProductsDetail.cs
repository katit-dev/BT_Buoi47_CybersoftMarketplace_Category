using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class VGetAllProductsDetail
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int ProductVariantId { get; set; }

    public string? ProductVariantName { get; set; }

    public int ShopId { get; set; }

    public string ShopName { get; set; } = null!;
}
