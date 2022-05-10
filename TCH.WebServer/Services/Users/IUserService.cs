using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Users
{
    public interface IUserService
    {
        Task<ResponseLogin<ApplicationUser>> GetUserInfo(); 
    }
}
