using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface ICustomer1Repository : IRepositoryBase<Customer1>
{
}

public class Customer1Repository : RepositoryBase<Customer1>, ICustomer1Repository
{
    public Customer1Repository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
