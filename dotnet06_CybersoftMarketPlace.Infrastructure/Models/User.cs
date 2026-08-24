using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class User
{
    public Guid Id { get; set; }

    public string Username { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? Alias { get; set; }

    public string Email { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Avatar { get; set; }

    public string PasswordHash { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Conversation> Conversations { get; set; } = new List<Conversation>();

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
