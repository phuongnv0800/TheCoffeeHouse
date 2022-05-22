using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Text.Json;
using TCH.Utilities.SubModels;

namespace TCH.WebServer.Services;

public class BaseApiClient
{
    private readonly HttpClient httpClient;
    private readonly ILocalStorageService localStorage;

    protected BaseApiClient(HttpClient httpClient, ILocalStorageService localStorage)
    {
        this.httpClient = httpClient;
        this.localStorage = localStorage;
    }

    protected async Task<T> GetFromJsonAsync<T>(string url)
    {
        var token = await localStorage.GetItemAsync<string>("authToken");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await httpClient.GetFromJsonAsync<T>(url);

        return response;
    }

    public async Task<MessageResult> DeleteAsync(string url)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await httpClient.DeleteAsync(url);
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {

        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }

    public async Task<MessageResult> UpdateAsJsonAsync<T>(string url, T request)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PutAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }
    public async Task<MessageResult> UpdateAsync(string url, MultipartFormDataContent request)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PutAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }

    public async Task<MessageResult> CreateAsJsonAsync<T>(string url, T request)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }
    public async Task<MessageResult> CreateNoBodyAsJsonAsync(string url)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsJsonAsync(url, "");
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }
    public async Task<MessageResult> CreateAsync(string url, MultipartFormDataContent request)
    {
        try
        {
            var token = await localStorage.GetItemAsync<string>("authToken");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await httpClient.PostAsync(url, request);
            if (response.IsSuccessStatusCode)
            {
                //var body = await response.Content.ReadAsStringAsync();
                //return JsonSerializer.Deserialize<MessageResult>(body);
                var body = await response.Content.ReadFromJsonAsync<MessageResult>();
                return body;
            }
        }
        catch (Exception e)
        {
        }
        return new MessageResult()
        {
            Result = 0,
            Message = "Failed",
        };
    }

    //public async Task<List<T>> GetListAsync<T>(string url, bool requiredLogin = false)
    //{
    //    var sessions = _httpContextAccessor
    //       .HttpContext
    //       .Session
    //       .GetString(SystemConstants.AppSettings.Token);
    //    var client = _httpClientFactory.CreateClient();
    //    client.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
    //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

    //    var response = await client.GetAsync(url);
    //    var body = await response.Content.ReadAsStringAsync();
    //    if (response.IsSuccessStatusCode)
    //    {
    //        var data = (List<T>)JsonConvert.DeserializeObject(body, typeof(List<T>));
    //        return data;
    //    }
    //    throw new Exception(body);
    //}
}
