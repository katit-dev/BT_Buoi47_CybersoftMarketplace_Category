using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Conversation
{
    public int Id { get; set; }

    public Guid BuyerId { get; set; }

    public int ShopId { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual User Buyer { get; set; } = null!;

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual Shop Shop { get; set; } = null!;
}
