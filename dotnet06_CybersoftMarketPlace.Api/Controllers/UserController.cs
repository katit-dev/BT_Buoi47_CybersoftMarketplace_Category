using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Infrastructure.Models;
using backend_netcore_dotnet06.Helper;
namespace dotnet06_CybersoftMarketPlace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;
        //Chỉ làm việc với service, không làm việc trực tiếp với repository
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Tạo user từ view đăng ký tài khoản người dùng
        /// </summary>
        /// <param name="model">Thông tin đăng ký của người dùng</param>
        /// <returns>Trả về kết quả đăng ký</returns>
        [ProducesResponseType(
            StatusCodes.Status201Created,
            Type = typeof(HTTPResponseData<string>)
        )]
        [ProducesResponseType(
            StatusCodes.Status400BadRequest,
            Type = typeof(HTTPResponseData<string>)
        )]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(UserRegisterDTO model)
        {
            HTTPResponseData<string>? response =
                await _userService.RegisterUserAsync(model);

            return StatusCode(response.statusCode, response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO model)
        {
            HTTPResponseData<string>? response =
                await _userService.LoginUserAsync(model);

            return StatusCode(response.statusCode, response);
        }


    }
}