using Blazored.LocalStorage;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services;

public interface IRoleApiClient
{
    Task<Respond<PagedList<RoleVm>>> GetAll();
}

public class RoleApiClient : BaseApiClient, IRoleApiClient
{
    public RoleApiClient(HttpClient httpClient, ILocalStorageService localStorage) : base(httpClient, localStorage)
    {
    }
    public async Task<Respond<PagedList<RoleVm>>> GetAll() => await GetFromJsonAsync<Respond<PagedList<RoleVm>>>($"api/roles");
}
