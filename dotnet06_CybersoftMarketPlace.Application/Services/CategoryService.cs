using backend_netcore_dotnet06.Helper;
using Infrastructure.Models;
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

    //create category
    public async Task<HTTPResponseData<string>> CreateCategoryAsync(
    CategoryCreateDTO model
)
    {
        try
        {
            var categoryRepository =
                _uniOfWork.CategoryRepository;

            // Kiểm tra trùng Name trong cùng Shop
            var existingCategory =
                await categoryRepository.SingleOrDefault(
                    c =>
                        c.Name == model.Name &&
                        c.ShopId == model.ShopId
                );

            if (existingCategory != null)
            {
                return new HTTPResponseData<string>
                {
                    DataResponse =
                        CategoryResponseMessageDTO.CategoryAlreadyExists,

                    Message =
                        CategoryResponseMessageDTO.CategoryAlreadyExists,

                    statusCode = 400,

                    Timestamp = DateTime.Now
                };
            }

            // DTO -> Entity
            var category = new Category
            {
                Name = model.Name,

                ShopId = model.ShopId,

                Alias =
                    HelperFunction.StringToSlug(model.Name),

                Deleted = false
            };

            // Thêm vào database thông qua Repository
            await categoryRepository.AddAsync(category);

            return new HTTPResponseData<string>
            {
                DataResponse =
                    CategoryResponseMessageDTO.CreateSuccess,

                Message =
                    CategoryResponseMessageDTO.CreateSuccess,

                statusCode = 201,

                Timestamp = DateTime.Now
            };
        }
        catch (Exception)
        {
            await _uniOfWork.RollbackTransactionAsync();

            return new HTTPResponseData<string>
            {
                DataResponse =
                    CategoryResponseMessageDTO.CreateFailed,

                Message =
                    CategoryResponseMessageDTO.CreateFailed,

                statusCode = 400,

                Timestamp = DateTime.Now
            };
        }
    }
}