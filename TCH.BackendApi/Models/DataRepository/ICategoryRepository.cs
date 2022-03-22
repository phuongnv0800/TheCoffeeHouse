using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface ICategoryRepository
    {
        Task<MessageResult> Create(CategoryVm request);
        Task<MessageResult> Delete(string id);
        Task<Respond<PagedList<CategoryVm>>> GetAll(Search request);
        Task<MessageResult> Update(string id, CategoryVm name);
    }
}