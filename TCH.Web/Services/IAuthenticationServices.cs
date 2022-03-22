using TCH.Web.Models;

namespace TCH.Web.Services
{
    public interface IAuthenticationServices
    {
        public Task<ResponseLogin<string>> Login(RequestLogin requestLogin);

        public Task Logout();
    }
}
