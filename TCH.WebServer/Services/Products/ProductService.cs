using Blazored.LocalStorage;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Products
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public ProductService(HttpClient httpClient, 
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public Task<Product> AddProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProduct(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetProductByName(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> GetProducts(bool IsPaging, int pageSize, int pageNumber)
        {
            try
            {
                List<Product> Products;
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await _httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Product>>>("/api/Products?IsPging=" + IsPaging.ToString() 
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
                if (response.Result != 1)
                {
                    return null;
                }
                Products = response.Data.Items;
                return Products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<Product> UpdateProduct(string id)
        {
            throw new NotImplementedException();
        }
    }
}
