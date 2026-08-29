using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Lấy danh sách category, hỗ trợ tìm kiếm và phân trang
    /// </summary>
    /// <param name="keyword">Từ khóa tìm kiếm theo tên category</param>
    /// <param name="pageIndex">Trang hiện tại</param>
    /// <param name="pageSize">Số lượng category mỗi trang</param>
    /// <returns>Danh sách category</returns>
    [HttpGet("GetAll")]
    [ProducesResponseType(
        typeof(HTTPResponseData<List<CategoryDTO>>),
        StatusCodes.Status200OK
    )]
    public async Task<IActionResult> GetAll(
        string keyword = "",
        int pageIndex = 1,
        int pageSize = 10
    )
    {
        var response = await _categoryService.GetAllCategoriesAsync(
            keyword,
            pageIndex,
            pageSize
        );

        return StatusCode(
            response.statusCode,
            response
        );
    }

    /// <summary>
    /// Create a new category
    /// </summary>
    [HttpPost("Create")]
    [ProducesResponseType(
        typeof(HTTPResponseData<string>),
        StatusCodes.Status201Created
    )]
    [ProducesResponseType(
        typeof(HTTPResponseData<string>),
        StatusCodes.Status400BadRequest
    )]
    public async Task<IActionResult> Create(
        [FromBody] CategoryCreateDTO model
    )
    {
        var response =
            await _categoryService.CreateCategoryAsync(model);

        return StatusCode(
            response.statusCode,
            response
        );
    }
}