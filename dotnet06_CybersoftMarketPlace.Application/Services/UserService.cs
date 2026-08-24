


using backend_netcore_dotnet06.Helper;
using Infrastructure.Models;
using Infrastructure.Repositories;
public interface IUserService
{
    public Task<HTTPResponseData<string>> RegisterUserAsync(UserRegisterDTO model);
    public Task<HTTPResponseData<string>> LoginUserAsync(UserLoginDTO model);
}

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly JwtAuthService _jwtAuthService;

    //Trong tầng service sẽ gọi các repository để xử lý

    public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository, IUserRoleRepository userRoleRepository, JwtAuthService jwtAuthService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _jwtAuthService = jwtAuthService;
    }

    public async Task<HTTPResponseData<string>> LoginUserAsync(UserLoginDTO model)
    {
        string? token = await _jwtAuthService.GenerateToken(model);
        if (token == null)
        {
            return new HTTPResponseData<string>
            {
                DataResponse = UserResponseMessageDTO.UserNotFound,
                Message = UserResponseMessageDTO.UserNotFound,
                statusCode = 401,
                Timestamp = DateTime.Now
            };
        }

        return new HTTPResponseData<string>
        {
            DataResponse = token,
            Message = UserResponseMessageDTO.SuccessLogin,
            statusCode = 200,
            Timestamp = DateTime.Now
        };
    }


    public async Task<HTTPResponseData<string>> RegisterUserAsync(UserRegisterDTO model)
    {
        try
        {
            // 1/ insert vào bảng user
            User userModel = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                FullName = model.FullName,
                Alias = HelperFunction.StringToSlug(model.FullName),
                Email = model.Email,
                Phone = model.Phone,
                Avatar = @$"https://ui-avatars.com/api/?name={model.FullName}&background=random&size=128",
                PasswordHash = HelperFunction.HashPassword(model.Password),
                Address = model.Address,
                CreatedAt = DateTime.Now,
                Deleted = false,
            };

            //1.1 check email trùng
            var existingUserByEmail = await _userRepository.SingleOrDefault(u => u.Email == model.Email || u.Username == model.Username || u.Phone == model.Phone);
            if (existingUserByEmail != null)
            {
                return new HTTPResponseData<string>
                {
                    DataResponse = UserResponseMessageDTO.EmailUsernameOrPhoneExists,
                    Message = UserResponseMessageDTO.EmailUsernameOrPhoneExists,
                    statusCode = 400,
                    Timestamp = DateTime.Now
                };
            }

            await _userRepository.AddAsync(userModel);
            //Add liên bảng userole
            UserRole userRoleModel = new UserRole
            {

                UserId = userModel.Id,
                RoleId = UserRoleConst.User, //Mặc định khi đăng ký sẽ là role user
            };
            userModel.UserRoles.Add(userRoleModel);
            //Sau khi thêm userrole từ usermodel thì savachangesAsync sẽ tự động thêm vào bảng userrole
            await _unitOfWork.SaveChangesAsync();
            return new HTTPResponseData<string>
            {
                DataResponse = UserResponseMessageDTO.SuccessRegister,
                Message = UserResponseMessageDTO.SuccessRegister,
                statusCode = 201,
                Timestamp = DateTime.Now
            };
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
        }
            return new HTTPResponseData<string>
            {
                DataResponse = UserResponseMessageDTO.FailedRegister,
                Message = UserResponseMessageDTO.FailedRegister,
                statusCode = 400,
                Timestamp = DateTime.Now
            };

    }
}