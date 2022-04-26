using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategories(bool IsPaging, int pageSize, int pageNumber);
        Task<Category> GetCategoryById(string id);
        Task<Category> GetCategoryByName(string name);
        Task<Category> AddCategory(Category product);
        Task<Category> UpdateCategory(Category category);
        Task DeleteCategory(string id);
    }
}
