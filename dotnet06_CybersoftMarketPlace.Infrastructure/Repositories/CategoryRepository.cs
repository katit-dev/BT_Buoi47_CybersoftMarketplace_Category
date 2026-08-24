using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface ICategoryRepository : IRepositoryBase<Category>
{
}

public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
{
    public CategoryRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
