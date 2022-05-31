using TCH.Utilities.SubModels;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.ViewModel.SubModels;
using TCH.Data.Entities;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IUserRepository
{
    Task<Respond<dynamic>> Authenicate(LoginRequest request);

    Task<Respond<AppUser>> GetById(string id);
    Task<Respond<UserVm>> GetByIdVm(string id);

    Task<Respond<AppUser>> GetByUserName(string userName);

    Task<Respond<PagedList<AppUser>>> GetAll(Search request);

    Task<MessageResult> Register(RegisterRequest request);
    Task<MessageResult> LockUser(string id);

    Task<MessageResult> Update(string id, UserUpdateRequest request);

    Task<MessageResult> RoleAssign(string id, RoleAssignRequest request);

    Task<MessageResult> Delete(string id);
    Task<MessageResult> ChangePasword(ChangePassword req);
    Task<Respond<PagedList<AppUser>>> GetAllByBranchID(string branchID, Search request);

}
