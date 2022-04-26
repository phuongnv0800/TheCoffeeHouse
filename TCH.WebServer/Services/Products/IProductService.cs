using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(bool IsPaging, int pageSize, int pageNumber);
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByName(string name);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(string id);
        Task DeleteProduct(string id);
    }
}
