using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IProductImageRepository : IRepositoryBase<ProductImage>
{
}

public class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
