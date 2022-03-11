using TCH.BackendApi.Models.System.Roles;

namespace TCH.BackendApi.Service.System
{
    public interface IRoleRepository
    {
        Task<List<RoleVm>> GetAll();
    }
}