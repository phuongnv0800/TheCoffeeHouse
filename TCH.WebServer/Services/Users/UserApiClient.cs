using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

public interface IUserApiClient
{
    Task<MessageResult> Assign(string id, RoleAssignRequest request);
    Task<MessageResult> Delete(string id);
    Task<Respond<PagedList<UserVm>>> GetAll(Search request);
    Task<Respond<UserVm>> GetById(string id);
    Task<Respond<UserVm>> GetByUserName(string userName);
    Task<MessageResult> Register(MultipartFormDataContent request);
    Task<MessageResult> Update(string id, MultipartFormDataContent request);
}

public class UserApiClient : BaseApiClient, IUserApiClient
{
    public UserApiClient(HttpClient httpClient, ILocalStorageService localStorageService)
        : base(httpClient, localStorageService)
    {
    }
    public async Task<Respond<PagedList<UserVm>>> GetAll(Search request)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = request.PageNumber.ToString(),
        };
        queryStringParam.Add("pageSize", request.PageSize.ToString());
        queryStringParam.Add("isPging", request.IsPging.ToString());
        if (!string.IsNullOrWhiteSpace(request.Name))
            queryStringParam.Add("name", request.Name);
        string url = QueryHelpers.AddQueryString($"/api/users/paging", queryStringParam);

        return await GetFromJsonAsync<Respond<PagedList<UserVm>>>(url);
    }
    public async Task<Respond<UserVm>> GetById(string id) => await GetFromJsonAsync<Respond<UserVm>>($"/api/users/{id}");
    public async Task<Respond<UserVm>> GetByUserName(string userName) => await GetFromJsonAsync<Respond<UserVm>>($"/api/users/name/{userName}");
    public async Task<MessageResult> Register(MultipartFormDataContent request) => await CreateAsync($"/api/users/create", request);
    public async Task<MessageResult> Update(string id, MultipartFormDataContent request) => await UpdateAsync($"/api/users/{id}", request);

    public async Task<MessageResult> Delete(string id) => await DeleteAsync($"/api/users/{id}");
    public async Task<MessageResult> LockUser(string id) => await CreateNoBodyAsJsonAsync($"/api/users/lock/{id}");
    public async Task<MessageResult> Assign(string id, RoleAssignRequest request) => await UpdateAsJsonAsync($"/api/users/assign-roles/{id}", request);
}
