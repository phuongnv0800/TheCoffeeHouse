using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;
using TCH.Data.Entities;
using TCH.Utilities.Enum;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

public interface IUserApiClient
{
    Task<MessageResult> Assign(string id, RoleAssignRequest request);
    Task<MessageResult> Delete(string id);
    Task<MessageResult> Lock(string id, Status status);
    Task<Respond<PagedList<AppUser>>> GetAll(Search request);
    Task<Respond<PagedList<AppUser>>> GetAllByBranch(string branchId, Search request);
    Task<Respond<AppUser>> GetById(string id);
    Task<Respond<UserVm>> GetByIdVm(string id);
    Task<Respond<AppUser>> GetByUserName(string userName);
    Task<MessageResult> Register(MultipartFormDataContent request);
    Task<MessageResult> Update(string id, MultipartFormDataContent request);
}

public class UserApiClient : BaseApiClient, IUserApiClient
{
    public UserApiClient(HttpClient httpClient, ILocalStorageService localStorageService)
        : base(httpClient, localStorageService)
    {
    }

    public async Task<MessageResult> Lock(string id, Status status)
        => await CreateAsJsonAsync($"/api/users/lock/{id}", status);

    public async Task<Respond<PagedList<AppUser>>> GetAll(Search request)
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

        return await GetFromJsonAsync<Respond<PagedList<AppUser>>>(url);
    }
    public async Task<Respond<PagedList<AppUser>>> GetAllByBranch(string branchId, Search request)
    {
        var queryStringParam = new Dictionary<string, string>
        {
            ["pageNumber"] = request.PageNumber.ToString(),
        };
        queryStringParam.Add("pageSize", request.PageSize.ToString());
        queryStringParam.Add("isPging", request.IsPging.ToString());
        if (!string.IsNullOrWhiteSpace(request.Name))
            queryStringParam.Add("name", request.Name);
        string url = QueryHelpers.AddQueryString($"/api/users/user-branch/{branchId}", queryStringParam);

        return await GetFromJsonAsync<Respond<PagedList<AppUser>>>(url);
    }

    public async Task<Respond<UserVm>> GetByIdVm(string id) =>
        await GetFromJsonAsync<Respond<UserVm>>($"/api/users/user-by/{id}");

    public async Task<Respond<AppUser>> GetById(string id) =>
        await GetFromJsonAsync<Respond<AppUser>>($"/api/users/{id}");

    public async Task<Respond<AppUser>> GetByUserName(string userName) =>
        await GetFromJsonAsync<Respond<AppUser>>($"/api/users/name/{userName}");

    public async Task<MessageResult> Register(MultipartFormDataContent request) =>
        await CreateAsync($"/api/users/create", request);

    public async Task<MessageResult> Update(string id, MultipartFormDataContent request) =>
        await UpdateAsync($"/api/users/{id}", request);

    public async Task<MessageResult> Delete(string id) => await DeleteAsync($"/api/users/{id}");
    public async Task<MessageResult> LockUser(string id) => await CreateNoBodyAsJsonAsync($"/api/users/lock/{id}");

    public async Task<MessageResult> Assign(string id, RoleAssignRequest request) =>
        await UpdateAsJsonAsync($"/api/users/assign-roles/{id}", request);
}