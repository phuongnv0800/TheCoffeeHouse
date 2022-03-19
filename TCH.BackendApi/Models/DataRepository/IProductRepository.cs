using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.ViewModel.Catalog;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IProductRepository
    {
        Task<int> AddImage(int productId, ProductImageRequest request);
        Task<bool> CategoryAssign(int id, int categoryId);
        Task<int> Create(ProductRequest request);
        Task<int> Delete(int productId);
        Task<PagedList<ProductVm>> GetAll(Search request, int categoryId = 0);
        Task<List<ProductImageVm>> GetAllImages(int productId);
        Task<ProductVm> GetById(int productId);
        Task<ProductImageVm> GetImageById(int imageId);
        Task<int> RemoveImage(int imageId);
        Task<int> Update(ProductRequest request);
        Task<int> UpdateImage(int imageId, ProductImageRequest request);
    }
}