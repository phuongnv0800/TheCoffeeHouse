using System.Security.Claims;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

public interface IAuthService
{
    Task<bool> Login(LoginRequest loginRequest);
    Task Logout();
    IEnumerable<Claim> GetClaims();
}
