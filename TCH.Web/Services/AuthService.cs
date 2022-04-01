using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using TCH.Web.Models;

namespace TCH.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        public IEnumerable<Claim> Claims { get; set; }

        public AuthService(HttpClient httpClient, AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }
        public async Task<bool> Login(LoginRequest loginRequest)
        {

            var result = await _httpClient.PostAsJsonAsync("/api/Users/authenticate", loginRequest);
            if (!result.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await result.Content.ReadAsStringAsync();
            ResponseLogin<string> respond = JsonConvert.DeserializeObject<ResponseLogin<string>>(content);
            Claims = ((ApiAuthenticationStateProvider)_authenticationStateProvider).ParseClaimsFromJwt(respond.Data);
            await _localStorage.SetItemAsync("authToken", content);
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.UserName);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", content);
            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public IEnumerable<Claim> GetClaims()
        {
            return this.Claims;
        }
    }
    public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}