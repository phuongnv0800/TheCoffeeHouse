using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Recipes
{
    public interface IRecipeService
    {
        Task<ResponseLogin<PagedList<RecipeDetail>>> GetAllRecipe(bool IsPaging, int pageSize, int pageNumber, string name);
        Task<Respond<IEnumerable<RecipeDetail>>> GetAllRecipeByProductId(string id);
        Task<ResponseLogin<string>> AddRecipe(List<RecipeRequest> requests);
    }
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ResponseLogin<string>> AddRecipe(List<RecipeRequest> requests)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(requests), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync($"/api/Recipes/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<string> respond = JsonConvert.DeserializeObject<ResponseLogin<string>>(content);
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

        public async Task<ResponseLogin<PagedList<RecipeDetail>>> GetAllRecipe(bool IsPaging, int pageSize, int pageNumber, string name)
        {
            try
            {
                List<RecipeDetail> Products;
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _httpClient.GetFromJsonAsync<ResponseLogin<PagedList<RecipeDetail>>>("/api/Recipes?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + "&Name=" + name);
                if (response.Result != 1)
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Respond<IEnumerable<RecipeDetail>>> GetAllRecipeByProductId(string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await _httpClient.GetAsync($"/api/Recipes/recipe-product/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Respond<IEnumerable<RecipeDetail>> respond = JsonConvert.DeserializeObject<Respond<IEnumerable<RecipeDetail>>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
