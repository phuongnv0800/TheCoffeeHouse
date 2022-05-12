using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Products
{
    public interface IProductService
    {
        Task<ResponseLogin<PagedList<Product>>> GetProducts(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<Product>> GetProductById(string id);
        Task<ResponseLogin<PagedList<Product>>> GetProductByCategoryId(string id);
        Task<Product> GetProductByName(string name);
        Task<ResponseLogin<Product>> AddProduct(Product product);
        Task<ResponseLogin<Product>> UpdateProduct(Product product);
        Task DeleteProduct(string id);
    }
}
