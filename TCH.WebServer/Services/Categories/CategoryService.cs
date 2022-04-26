using System.Net.Http.Headers;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<Category> AddCategory(Category product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> GetCategories(bool IsPaging, int pageSize, int pageNumber)
        {
            try
            {
                List<Category> categories;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Category>>>("/api/Products?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
                if (response.Result != 1)
                {
                    return null;
                }
                categories = response.Data.Items;
                return categories;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Category> GetCategoryById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Category> GetCategoryByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<Category> UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
