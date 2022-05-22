using TCH.Utilities.SubModels;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IUserRepository
{
    Task<Respond<dynamic>> Authenicate(LoginRequest request);

    Task<Respond<UserVm>> GetById(string id);

    Task<Respond<UserVm>> GetByUserName(string userName);

    Task<Respond<PagedList<UserVm>>> GetAll(Search request);

    Task<MessageResult> Register(RegisterRequest request);
    Task<MessageResult> LockUser(string id);

    Task<MessageResult> Update(string id, UserUpdateRequest request);

    Task<MessageResult> RoleAssign(string id, RoleAssignRequest request);

    Task<MessageResult> Delete(string id);
    Task<MessageResult> ChangePasword(ChangePassword req);
    Task<Respond<PagedList<UserVm>>> GetAllByBranchID(string branchID, Search request);

}
