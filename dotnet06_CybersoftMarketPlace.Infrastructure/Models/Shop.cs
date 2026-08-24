using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Shop
{
    public int Id { get; set; }

    public Guid OwnerId { get; set; }

    public string? Image { get; set; }

    public string ShopName { get; set; } = null!;

    public string? Description { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual User Owner { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
