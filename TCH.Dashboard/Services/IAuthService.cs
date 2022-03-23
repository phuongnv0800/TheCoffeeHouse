namespace TCH.Dashboard.Services
{
    public interface IAuthService
    {
        Task<bool> Login(LoginRequest loginRequest);
        Task Logout();
    }
}
