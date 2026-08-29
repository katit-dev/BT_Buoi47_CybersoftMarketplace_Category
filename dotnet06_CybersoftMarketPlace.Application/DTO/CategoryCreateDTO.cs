using System.ComponentModel.DataAnnotations;

public class CategoryCreateDTO
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = null!;

    [Required]
    public int ShopId { get; set; }
}