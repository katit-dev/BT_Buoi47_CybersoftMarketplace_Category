


using Infrastructure.Models;
public class ProductIndexPageDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; } = "https://via.placeholder.com/150"; // Placeholder image URL
    public ProductCategoryIndexPageDTO ProductCategory { get; set; } = new ProductCategoryIndexPageDTO();
    public decimal? Price { get; set; }

    public ShopProductIndexPageDTO Shop { get; set; } = new ShopProductIndexPageDTO();


}

/* Json mẫu của ProductIndexPageDTO 
    {
        "id": 1,
        "name": "Sản phẩm 1",
        "productCategory": {
            "id": 1,
            "name": "Danh mục sản phẩm 1",
            "description": "Mô tả danh mục sản phẩm 1"
        },
        "price": 100000,
        "shop": {
            "id": 1,
            "name": "Cửa hàng 1"
        }
    }

*/

public class ProductCategoryIndexPageDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}


public class ShopProductIndexPageDTO 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}