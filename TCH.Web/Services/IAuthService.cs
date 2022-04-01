using System.Security.Claims;

namespace TCH.Web.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task Logout();
        IEnumerable<Claim> GetClaims();
    }
}
