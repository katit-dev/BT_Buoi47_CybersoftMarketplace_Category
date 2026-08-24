using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Message
{
    public int Id { get; set; }

    public int ConversationId { get; set; }

    public Guid SenderId { get; set; }

    public string Content { get; set; } = null!;

    public DateTime? SentAt { get; set; }

    public string? Alias { get; set; }

    public string? AdditionalData { get; set; }

    public bool? Deleted { get; set; }

    public virtual Conversation Conversation { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
