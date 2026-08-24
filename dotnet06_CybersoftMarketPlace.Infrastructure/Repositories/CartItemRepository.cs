using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface ICartItemRepository : IRepositoryBase<CartItem>
{
}

public class CartItemRepository : RepositoryBase<CartItem>, ICartItemRepository
{
    public CartItemRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
