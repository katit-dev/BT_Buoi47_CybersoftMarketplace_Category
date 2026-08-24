using Infrastructure.Models;

public interface IProductRepository : IRepositoryBase<Product>
{
    // Đã có các method từ IRepositoryBase<Product>.
    // Khai báo thêm các method riêng của Product tại đây.

}

public class ProductRepository
    : RepositoryBase<Product>, IProductRepository
{
    public ProductRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }

    // Cài đặt thêm các method riêng của Product tại đây.
}