using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface ICartRepository : IRepositoryBase<Cart>
{
}

public class CartRepository : RepositoryBase<Cart>, ICartRepository
{
    public CartRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
