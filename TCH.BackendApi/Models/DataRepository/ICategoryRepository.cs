using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.ViewModel.Catalog;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface ICategoryRepository
    {
        Task<MessageResult> Create(string name);
        Task<MessageResult> Delete(string id);
        Task<Respond<PagedList<CategoryVm>>> GetAll(Search request);
        Task<MessageResult> Update(string id, string name);
    }
}