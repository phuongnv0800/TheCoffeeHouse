using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

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
    public async Task<Respond<dynamic>> Login(LoginRequest loginRequest)
    {

        var result = await _httpClient.PostAsJsonAsync("/api/Users/authenticate", loginRequest);
        if (!result.IsSuccessStatusCode)
        {
            return new Respond<dynamic>()
            {
                Message = "Thất bại",
                Result = -1,
                Data = false,
            };
        }
        var content = await result.Content.ReadAsStringAsync();
        Respond<string> respond = JsonConvert.DeserializeObject<Respond<string>>(content);
        if (respond?.Result != 1 && respond != null)
        {
            return new Respond<dynamic>()
            {
                Message = respond.Message,
                Result = respond.Result,
                Data = false,
            };
        }
        Claims = ((ApiAuthenticationStateProvider)_authenticationStateProvider).ParseClaimsFromJwt(respond.Data);
        await _localStorage.SetItemAsync("authToken", respond.Data);
        ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginRequest.UserName);
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", respond.Data);
        return new Respond<dynamic>()
        {
            Message = respond.Message,
            Result = respond.Result,
            Data = true,
        };
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