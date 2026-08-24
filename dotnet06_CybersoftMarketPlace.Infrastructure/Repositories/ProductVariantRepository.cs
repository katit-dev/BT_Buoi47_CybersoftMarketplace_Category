using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IProductVariantRepository : IRepositoryBase<ProductVariant>
{
}

public class ProductVariantRepository : RepositoryBase<ProductVariant>, IProductVariantRepository
{
    public ProductVariantRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
