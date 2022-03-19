using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.Models.System;

namespace TCH.BackendApi.Service.System
{
    public interface IRoleRepository
    {
        Task<Respond<PagedList<RoleVm>>> GetAll(Search search);
    }
}