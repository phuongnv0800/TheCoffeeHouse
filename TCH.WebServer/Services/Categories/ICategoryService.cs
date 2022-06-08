using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public interface ICategoryService
    {
        Task<ResponseLogin<PagedList<Category>>> GetCategories(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<PagedList<Category>>> GetAllCategories();
        Task<ResponseLogin<Category>> AddCategory(Category product);
        Task<ResponseLogin<Category>> GetCategoryById(string id);
        Task<ResponseLogin<Category>> UpdateCategory(Category category);
        Task DeleteCategory(string id);
    }
}
