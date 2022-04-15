using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface ICategoryRepository
{
    Task<MessageResult> Create(CategoryVm request);
    Task<MessageResult> Delete(string id);
    Task<Respond<PagedList<CategoryVm>>> GetAll(Search request);
    Task<MessageResult> Update(string id, CategoryVm name);
}
