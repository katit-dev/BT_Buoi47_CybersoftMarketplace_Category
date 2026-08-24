using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Order
{
    public int Id { get; set; }

    public Guid BuyerId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
