using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Products
{
    public interface IProductService
    {
        Task<ResponseLogin<PagedList<ProductVm>>> GetProducts(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<ProductVm>> GetProductById(string id);
        Task<ResponseLogin<PagedList<ProductVm>>> GetProductByCategoryId(string id);
        Task<ProductVm> GetProductByName(string name);
        Task<ResponseLogin<ProductVm>> AddProduct(MultipartFormDataContent product);
        Task<ResponseLogin<ProductVm>> UpdateProduct(MultipartFormDataContent product);
        Task DeleteProduct(string id);
    }
}
