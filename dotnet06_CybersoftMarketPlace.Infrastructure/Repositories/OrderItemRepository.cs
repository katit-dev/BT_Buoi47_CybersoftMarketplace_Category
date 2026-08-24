using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IOrderItemRepository : IRepositoryBase<OrderItem>
{
}

public class OrderItemRepository : RepositoryBase<OrderItem>, IOrderItemRepository
{
    public OrderItemRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
