using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Users
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        private IHttpContextAccessor _httpContext;

        public UserService(HttpClient httpClient, IHttpContextAccessor httpContext)
        {
            _httpClient = httpClient;
            _httpContext = httpContext;
        }

        public Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUserBranchRole()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseLogin<PagedList<ApplicationUser>>> GetAllUserManageRole()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseLogin<ApplicationUser>> GetUserInfo()
        {
            try
            {
                if (GbParameter.GbParameter.UserId == null)
                {
                    return new ResponseLogin<ApplicationUser> { Result = 2, Data = null };
                }
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await _httpClient.GetFromJsonAsync<ResponseLogin<ApplicationUser>>($"/api/Users/{GbParameter.GbParameter.UserId}");
                if ((int)response.Result == 1)
                {
                    return response;
                    //ResponseLogin<ApplicationUser> respond = JsonConvert.DeserializeObject<ResponseLogin<ApplicationUser>>(content);
                    //if (respond.Result == 1)
                    //{
                    //    return respond;
                    //}
                    //return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
