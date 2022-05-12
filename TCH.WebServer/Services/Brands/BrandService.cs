using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Brands
{
    public interface IBrandService
    {
        Task<ResponseLogin<PagedList<Branch>>> GetAllBranchs();
        Task<ResponseLogin<Branch>> AddBranch(Branch branch);
        Task DeleteBranch(string id);
    }
    public class BrandService: IBrandService
    {
        private readonly HttpClient httpClient;

        public BrandService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Branch>> AddBranch(Branch branch)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Categories/", httpContent);
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

        public Task DeleteBranch(string id)
        {
            throw new NotImplementedException();
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
    }
}
