using TCH.WebServer.Models;

namespace TCH.WebServer.Services
{
    public interface IAuthenticationServices
    {
        public Task<ResponseLogin<string>> Login(RequestLogin requestLogin);

        public Task Logout();
    }
}
