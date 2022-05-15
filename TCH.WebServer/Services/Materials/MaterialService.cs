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
        Task<ResponseLogin<PagedList<MaterialType>>> GetMaterialTypes();
        Task<ResponseLogin<Material>> AddMaterial(MultipartFormDataContent material);
        Task<ResponseLogin<Material>> UpdateMaterial(MultipartFormDataContent material);
        Task<ResponseLogin<Material>> GetMaterialById(string id);
        Task DeleteMaterial(string id);
    }
    public class MaterialService : IMaterialService
    {
        private readonly HttpClient httpClient;

        public MaterialService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Material>> AddMaterial(MultipartFormDataContent material)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(material), Encoding.UTF8, "application/json");
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

        public async Task DeleteMaterial(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Materials/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Material>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Material>>>(content);
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

        public async Task<ResponseLogin<Material>> UpdateMaterial(MultipartFormDataContent material)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Branchs/", material);
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
        public async Task<ResponseLogin<Material>> GetMaterialById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Materials/{id}");
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

        public async Task<ResponseLogin<PagedList<MaterialType>>> GetMaterialTypes()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<MaterialType>>>("/api/Materials/type");
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
