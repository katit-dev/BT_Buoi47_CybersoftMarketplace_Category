using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Infrastructure.Repositories;
using Infrastructure.Models;
using backend_netcore_dotnet06.Helper;  
namespace dotnet06_CybersoftMarketPlace.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;
        //Chỉ làm việc với service, không làm việc trực tiếp với repository
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        /// <summary>
        /// Lấy danh sách sản phẩm
        /// </summary>
        /// <param name="keyword">Từ khóa tìm kiếm sản phẩm</param>
        /// <param name="pageIndex">Chỉ số trang</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang</param>
        /// <returns>Trả về danh sách sản phẩm</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(HTTPResponseData<List<ProductIndexPageDTO>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(HTTPResponseData<string>))]

        [HttpGet("GetAll")]
        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetAllProducts(string keyword = "", int pageIndex = 1, int pageSize = 10)
        {
            HTTPResponseData<List<ProductIndexPageDTO>>? response = await _productService.GetAllProductsAsync(keyword, pageIndex, pageSize);
            return StatusCode(response.statusCode, response);
        }
        

        
    }
}