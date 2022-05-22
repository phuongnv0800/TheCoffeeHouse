using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Users
{
    public interface IUserService
    {
        Task<ResponseLogin<ApplicationUser>> GetUserInfo();
        Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUserBranchRole();
        Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUserManageRole();
        Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUser();
    }
}
