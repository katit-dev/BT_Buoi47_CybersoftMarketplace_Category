using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public interface ICategoryService
{
    Task<HTTPResponseData<List<CategoryDTO>>> GetAllCategoriesAsync(string keyword = "", int pageIndex = 1, int pageSize = 10);
    Task<HTTPResponseData<string>> CreateCategoryAsync(CategoryCreateDTO model);
}

public class CategoryService : ICategoryService
{
    // khai bao UnitOfWork
    private readonly IUnitOfWork _uniOfWork;

    // constructor
    public CategoryService(IUnitOfWork unitOfWork)
    {
        _uniOfWork = unitOfWork;
    }

    // Get All Categories
    public async Task<HTTPResponseData<List<CategoryDTO>>> GetAllCategoriesAsync(string keyword = "", int pageIndex = 1, int pageSize = 10)
    {
        // lay catrgoryRepository tu UnitOfWork
        var categoryRepository = _uniOfWork.CategoryRepository;

        // tranh keyword bi null
        keyword ??= "";

        // Query(lay ra) cac catgory chua bi xoa
        var query = categoryRepository.WhereSql(c => c.Deleted == false);

        // sau do, neu co keyword thi tim theo name
        if (!string.IsNullOrWhiteSpace(keyword))
        {
            query = query.Where(
                c => c.Name.Contains(keyword)
            );
        }

        // Map entity => dto + phan trang
        var categories = await query
    .Select(c => new CategoryDTO
    {
        Id = c.Id,
        Name = c.Name,
        Alias = c.Alias
    })
    .Skip((pageIndex - 1) * pageSize)
    .Take(pageSize)
    .ToListAsync();

        return new HTTPResponseData<List<CategoryDTO>>
        {
            DataResponse = categories,
            Message = "Get categories successfully",
            statusCode = 200,
            Timestamp = DateTime.Now
        };
    }
}