public interface ICategoryService
{
    Task<HTTPResponseData<List<CategoryDTO>>> GetAllCategoriesAsync(string keyword = "", int pageIndex = 1, int pageSize = 10);
}

