using System.Security.Claims;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

public interface IAuthService
{
    Task<Respond<dynamic>> Login(LoginRequest loginRequest);
    Task Logout();
    IEnumerable<Claim> GetClaims();
}
