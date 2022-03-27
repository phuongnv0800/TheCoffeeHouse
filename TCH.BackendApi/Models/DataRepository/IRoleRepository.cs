using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface IRoleRepository
{
    Task<Respond<PagedList<RoleVm>>> GetAll(Search search);
}
