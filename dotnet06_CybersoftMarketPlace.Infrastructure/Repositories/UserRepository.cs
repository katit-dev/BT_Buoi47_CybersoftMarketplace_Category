// Nơi định nghĩa các phương thức truy xuất dữ liệu liên quan đến User

using Infrastructure.Models;

namespace Infrastructure.Repositories;

public interface IUserRepository : IRepositoryBase<User>
{
    // Đã có các method từ IRepositoryBase<User>.
    // Khai báo thêm các method riêng của User tại đây.

}

public class UserRepository
    : RepositoryBase<User>, IUserRepository
{
    public UserRepository(CybersoftMarketPlaceContext context)
        : base(context)
    {
    }

    // Cài đặt thêm các method riêng của User tại đây.
}