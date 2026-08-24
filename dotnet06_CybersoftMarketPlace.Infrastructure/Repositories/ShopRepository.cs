using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IShopRepository : IRepositoryBase<Shop>
{
}

public class ShopRepository : RepositoryBase<Shop>, IShopRepository
{
    public ShopRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
