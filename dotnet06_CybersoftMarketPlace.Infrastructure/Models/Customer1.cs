using System;
using System.Collections.Generic;

namespace Infrastructure.Models;

public partial class Customer1
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Alias { get; set; }

    public int? Age { get; set; }

    public string? City { get; set; }

    public string? Aditional { get; set; }

    public bool? Deleted { get; set; }

    public DateTime? DateCreate { get; set; }
}
