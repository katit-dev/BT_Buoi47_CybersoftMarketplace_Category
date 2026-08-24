using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface ICustomerRepository : IRepositoryBase<Customer>
{
}

public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
{
    public CustomerRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
