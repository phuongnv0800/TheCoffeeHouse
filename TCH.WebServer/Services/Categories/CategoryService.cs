using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Blazored.LocalStorage;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public class CategoryService : BaseApiClient, ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient, ILocalStorageService localStorageService)
            : base(httpClient, localStorageService)
        {
            this.httpClient = httpClient;
        }

        public async Task<MessageResult> AddCategory(CategoryVm category)
        {
            try
            {
                var response = await CreateAsJsonAsync($"/api/Categories", category);
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<MessageResult> DeleteCategory(string id)
        {
            try
            {
                var response = await DeleteAsync($"/api/Categories/{id}");
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<PagedList<Category>>> GetAllCategories()
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Category>>>("/api/Categories");
            if (response.Result != 1)
            {
                return null;
            }

            return response;
        }

        public async Task<ResponseLogin<PagedList<Category>>> GetCategories(bool IsPaging, int pageSize, int pageNumber)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Category>>>(
                    "/api/Categories?IsPging=" + IsPaging.ToString()
                                               + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" +
                                               pageSize.ToString());
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

        public async Task<Respond<CategoryVm>> GetCategoryById(string id)
        {
            try
            {
                var response = await GetFromJsonAsync<Respond<CategoryVm>>($"/api/Categories/{id}");
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task<MessageResult> UpdateCategory(string id, CategoryVm category)
        {
            try
            {
                var response = await UpdateAsJsonAsync($"/api/Categories/{id}", category);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}