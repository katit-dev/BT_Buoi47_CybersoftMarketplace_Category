using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IRatingRepository : IRepositoryBase<Rating>
{
}

public class RatingRepository : RepositoryBase<Rating>, IRatingRepository
{
    public RatingRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}
