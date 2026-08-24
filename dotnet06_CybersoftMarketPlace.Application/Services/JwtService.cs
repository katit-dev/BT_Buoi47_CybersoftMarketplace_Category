using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Infrastructure.Models;
using backend_netcore_dotnet06.Helper;
public class JwtAuthService
{
    private readonly string? _key;
    private readonly string? _issuer;
    private readonly string? _audience;

    private readonly IUnitOfWork _unitOfWork;

    public JwtAuthService(IConfiguration Configuration, IUnitOfWork unitOfWork)
    {
        _key = Configuration["Jwt:Key"];
        _issuer = Configuration["Jwt:Issuer"];
        _audience = Configuration["Jwt:Audience"];
        _unitOfWork = unitOfWork;
    }
    
    public async Task<string?> GenerateToken(UserLoginDTO userLogin)
    {

        //Bước 1: kiểm tra db có email hoặc username hoặc phone đó không
        var userModel = _unitOfWork.UserRepository.SingleOrDefault(u => u.Email == userLogin.EmailOrPhoneOrUserName || u.Username == userLogin.EmailOrPhoneOrUserName || u.Phone == userLogin.EmailOrPhoneOrUserName).Result;

        if(userModel == null)
        {
             return null;
        }
        //Nếu mà có dữ liệu user đó
        if(!HelperFunction.VerifyPassword(userLogin.Password, userModel.PasswordHash))
        {
            return null;
        }

        


        // Khóa bí mật để ký token
        var key = Encoding.ASCII.GetBytes(_key);
        // Tạo danh sách các claims cho token
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Name, userModel.Id.ToString()), // Tên đầy đủ của người dùng
            new Claim("UserName", userModel.Username),               // Claim mặc định cho username
            // new Claim(ClaimTypes.Role, userLogin.Role),                   // Claim mặc định cho Role
            new Claim(JwtRegisteredClaimNames.Sub, userModel.Id.ToString()),   // Subject của token
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), // Unique ID của token
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), // Thời gian tạo token
            new Claim(JwtRegisteredClaimNames.Email, userModel.Email), // Email của người dùng
        };
        //Roles 
        IQueryable<UserRole>? userRoles = await _unitOfWork.UserRoleRepository.Where(u => u.UserId == userModel.Id);

        List<UserRole> userRolesList = userRoles.Select(n => new UserRole { Role = new Role { RoleName = n.Role.RoleName } }).ToList();



        foreach (UserRole userRole in userRolesList)
        {
            //Đưa role vào claim của token (mảng role)
            claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName.ToString()));
        }



        // Tạo khóa bí mật để ký token
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha256Signature
        );
        // Thiết lập thông tin cho token
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // Token hết hạn sau 1 ngày
            SigningCredentials = credentials,
            Issuer = _issuer,                 // Thêm Issuer vào token
            Audience = _audience,              // Thêm Audience vào token
        };
        // Tạo token bằng JwtSecurityTokenHandler
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        // Trả về chuỗi token đã mã hóa
        return tokenHandler.WriteToken(token);
    }

    public string DecodePayloadTokenId(string token)
    {
        try
        {
            // Kiểm tra token có null hoặc rỗng không
            if (string.IsNullOrEmpty(token))
            {
                throw new ArgumentException("Token không được để trống", nameof(token));
            }

            // Tạo handler và đọc token
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token);

            // Lấy userId từ claims (thường nằm trong claim "sub" hoặc "name")
            var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub); // Common in some identity providers

            if (userIdClaim == null)
            {
                throw new InvalidOperationException("Không tìm thấy userId trong payload");
            }

            return userIdClaim.Value;
        }
        catch (Exception ex)
        {
            // Xử lý lỗi (có thể log lỗi ở đây)
            throw new InvalidOperationException($"Lỗi khi decode token: {ex.Message}", ex);
        }
    }

}