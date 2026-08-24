using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IOrderRepository : IRepositoryBase<Order>
{
}

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
