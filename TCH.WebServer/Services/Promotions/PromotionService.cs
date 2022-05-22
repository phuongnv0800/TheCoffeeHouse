using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Promotions
{
    public interface IPromotionService
    {
        Task<ResponseLogin<PagedList<Promotion>>> GetAllPromotions(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<Promotion>> AddPromotion(PromotionRequest Promotion);
        Task<ResponseLogin<Promotion>> GetPromotionById(string id);
        Task<ResponseLogin<Promotion>> Update(PromotionRequest Promotion);
        Task DeletePromotion(string id);
    }
    public class PromotionService : IPromotionService
    {
        private readonly HttpClient httpClient;

        public PromotionService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Promotion>> AddPromotion(PromotionRequest Promotion)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Promotion), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Promotions/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Promotion> respond = JsonConvert.DeserializeObject<ResponseLogin<Promotion>>(content);
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

        public async Task DeletePromotion(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Promotions/{id}");
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
        public async Task<ResponseLogin<Promotion>> Update(PromotionRequest Promotion)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Promotion), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Promotions/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Promotion> respond = JsonConvert.DeserializeObject<ResponseLogin<Promotion>>(content);
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
        public async Task<ResponseLogin<PagedList<Promotion>>> GetAllPromotions(bool IsPaging, int pageSize, int pageNumber)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Promotion>>>("/api/Promotions?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }
        public async Task<ResponseLogin<Promotion>> GetPromotionById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Promotions/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Promotion> respond = JsonConvert.DeserializeObject<ResponseLogin<Promotion>>(content);
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
