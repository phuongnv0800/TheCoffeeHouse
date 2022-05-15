using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Materials
{
    public interface IMaterialService
    {
        Task<ResponseLogin<PagedList<Material>>> GetMaterials(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<Material>> AddMaterial(Material branch);
        Task DeleteMaterial(string id);
    }
    public class MaterialService : IMaterialService
    {
        private readonly HttpClient httpClient;

        public MaterialService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Material>> AddMaterial(Material branch)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(branch), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Materials/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Material> respond = JsonConvert.DeserializeObject<ResponseLogin<Material>>(content);
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

        public Task DeleteMaterial(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseLogin<PagedList<Material>>> GetMaterials(bool IsPaging, int pageSize, int pageNumber)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Material>>>("/api/Materials?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
                if (response.Result != 1)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
