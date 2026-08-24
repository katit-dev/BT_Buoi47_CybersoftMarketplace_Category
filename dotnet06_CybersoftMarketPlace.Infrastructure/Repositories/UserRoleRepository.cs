using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IUserRoleRepository : IRepositoryBase<UserRole>
{
    // Đã có các method từ IRepositoryBase<UserRole>.
    // Khai báo thêm các method đặc thù cho UserRole tại đây nếu cần.
}

public class UserRoleRepository
    : RepositoryBase<UserRole>, IUserRoleRepository
{
    public UserRoleRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }
}