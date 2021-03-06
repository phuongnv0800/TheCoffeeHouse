using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IRoleRepository
{
    Task<Respond<PagedList<RoleVm>>> GetAll(Search search);
}
