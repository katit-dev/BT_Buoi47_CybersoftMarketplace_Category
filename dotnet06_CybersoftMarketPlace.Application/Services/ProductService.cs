


using System.Collections.Generic;
using System.Linq;
using backend_netcore_dotnet06.Helper;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
public interface IProductService
{
    Task<HTTPResponseData<List<ProductIndexPageDTO>>> GetAllProductsAsync(string keyword = "", int pageIndex = 1, int pageSize = 10);
}

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<HTTPResponseData<List<ProductIndexPageDTO>>> GetAllProductsAsync(string keyword = "", int pageIndex = 1, int pageSize = 10)
    {
        //Lấy danh sách sản phẩm
        //Lấy repository từ UnitOfWork
        IProductRepository? productsRepo = _unitOfWork.ProductRepository;

        keyword = HelperFunction.StringToSlug(keyword);
        var products = await productsRepo.Where(p=>p.Deleted == false && p.Alias.Contains(keyword));

        Console.WriteLine($"Số lượng sản phẩm: {products.Count()}");

        //Phân trang
        if (products == null || products.Count() == 0)
        {
            return new HTTPResponseData<List<ProductIndexPageDTO>>
            {
                DataResponse = new List<ProductIndexPageDTO>(),
                Message = "Không có sản phẩm nào",
                statusCode = 200
            };
        }
        
        var data = products.Skip((pageIndex - 1) * pageSize).Take(pageSize).Select(p => new ProductIndexPageDTO
        {
            Id = p.Id,
            Name = p.Name,
            ImageUrl = p.ProductImages.FirstOrDefault().ImageUrl ?? "https://via.placeholder.com/150",
            ProductCategory = new ProductCategoryIndexPageDTO
            {
                Id = p.Category.Id,
                Name = p.Category.Name
            },
            Shop = new ShopProductIndexPageDTO
            {
                Id = p.Shop.Id,
                Name = p.Shop.ShopName,
                Description = p.Shop.Description ?? ""
            },
            Price = p.ProductVariants.FirstOrDefault().Price,
        }).ToList();

        return new HTTPResponseData<List<ProductIndexPageDTO>>
        {
            DataResponse = data,
            Message = "Lấy danh sách sản phẩm thành công",
            statusCode = 200
        };
    }

   

}