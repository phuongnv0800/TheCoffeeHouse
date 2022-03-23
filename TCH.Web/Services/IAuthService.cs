namespace TCH.Web.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
