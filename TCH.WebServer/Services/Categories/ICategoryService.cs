using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public interface ICategoryService
    {
        Task<ResponseLogin<PagedList<Category>>> GetCategories(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<PagedList<Category>>> GetAllCategories();
        Task<MessageResult> AddCategory(CategoryVm product);
        Task<Respond<CategoryVm>> GetCategoryById(string id);
        Task<MessageResult> UpdateCategory(string id, CategoryVm category);
        Task<MessageResult> DeleteCategory(string id);
    }
}
