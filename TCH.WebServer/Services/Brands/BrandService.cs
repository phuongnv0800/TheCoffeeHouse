using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Brands
{
    public interface IBrandService
    {
        Task<ResponseLogin<PagedList<Branch>>> GetAllBranchs();
        Task<ResponseLogin<Branch>> AddBranch(MultipartFormDataContent branch);
        Task<ResponseLogin<Branch>> GetBranchById(string id);
        Task<ResponseLogin<Branch>> Update(MultipartFormDataContent branch);
        Task DeleteBranch(string id);
    }
    public class BrandService: IBrandService
    {
        private readonly HttpClient httpClient;

        public BrandService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Branch>> AddBranch(MultipartFormDataContent branch)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Branchs/", branch);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Branch> respond = JsonConvert.DeserializeObject<ResponseLogin<Branch>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteBranch(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Branchs/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Product>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Product>>>(content);
                    if (respond.Result == 1)
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        public async Task<ResponseLogin<Branch>> Update(MultipartFormDataContent branch)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Branchs/", branch);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Branch> respond = JsonConvert.DeserializeObject<ResponseLogin<Branch>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<ResponseLogin<PagedList<Branch>>> GetAllBranchs()
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Branch>>>("/api/Branchs");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }
        public async Task<ResponseLogin<Branch>> GetBranchById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Branchs/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Branch> respond = JsonConvert.DeserializeObject<ResponseLogin<Branch>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
    }
}
