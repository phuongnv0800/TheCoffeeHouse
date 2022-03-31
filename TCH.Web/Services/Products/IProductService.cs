using TCH.Web.Models;

namespace TCH.Web.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(string id);
        Task<Product> GetProductByName(string name);
        Task<Product> AddProduct(Product product);
        Task<Product> UpdateProduct(string id);
        Task DeleteProduct(string id);
    }
}
