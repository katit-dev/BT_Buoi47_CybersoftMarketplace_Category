using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IRoleRepository : IRepositoryBase<Role>
{
}

public class RoleRepository : RepositoryBase<Role>, IRoleRepository
{
    public RoleRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
