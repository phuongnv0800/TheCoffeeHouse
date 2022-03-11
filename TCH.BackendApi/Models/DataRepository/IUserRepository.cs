using TCH.ViewModel.System.Users;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.Models.Common;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IUserRepository
    {
        Task<Respond<dynamic>> Authenicate(LoginRequest request);

        Task<Respond<UserVm>> GetById(string id);

        Task<Respond<UserVm>> GetByUserName(string userName);

        Task<PagedList<UserVm>> GetUsersPaging(PagingRequest request);

        Task<MessageResult> Register(RegisterRequest request);

        Task<MessageResult> Update(string id, UserUpdateRequest request);

        Task<MessageResult> RoleAssign(string id, RoleAssignRequest request);

        Task<MessageResult> Delete(string id);

    }
}