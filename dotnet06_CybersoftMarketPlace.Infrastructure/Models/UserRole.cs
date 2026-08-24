using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class UserRole
{
    public Guid UserId { get; set; }

    public int RoleId { get; set; }

    public string? Desc { get; set; }

    public virtual Role Role { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
