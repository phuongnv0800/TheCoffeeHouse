using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Products
{
    public interface IProductService
    {
        Task<ResponseLogin<PagedList<ProductVm>>> GetProducts(bool IsPaging, int pageSize, int pageNumber, string name);
        Task<ResponseLogin<PagedList<ProductVm>>> GetAllProducts();
        Task<ResponseLogin<PagedList<ProductVm>>> GetProductsByBranch(bool IsPaging, int pageSize, int pageNumber, string branchId, string name);
        Task<ResponseLogin<ProductVm>> GetProductById(string id);
        Task<ResponseLogin<PagedList<ProductVm>>> GetProductByCategoryId(string id);
        Task<PagedList<Size>> GetSizes();
        Task<PagedList<Topping>> GetToppings();
        Task<ResponseLogin<ProductVm>> AddProduct(MultipartFormDataContent product);
        Task<ResponseLogin<ProductVm>> UpdateProduct(MultipartFormDataContent product);
        Task<MessageResult> Update(string productId, bool isAvailable);
        Task DeleteProduct(string id);
    }
}
