using System.Net.Http.Headers;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Recipes
{
    public interface IRecipeService
    {
        Task<ResponseLogin<PagedList<RecipeDetail>>> GetAllRecipe(bool IsPaging, int pageSize, int pageNumber, string name);
        Task<ResponseLogin<PagedList<RecipeDetail>>> GetAllRecipeByProductId(string id);
        
    }
    public class RecipeService : IRecipeService
    {
        private readonly HttpClient _httpClient;

        public RecipeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        public async Task<ResponseLogin<PagedList<RecipeDetail>>> GetAllRecipeByProductId(string id)
        {
            try
            {
                List<RecipeDetail> Products;
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _httpClient.GetFromJsonAsync<ResponseLogin<PagedList<RecipeDetail>>>($"/api/Recipes/recipe-product/{id}");
                if (response.Result != 1)
                {
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
