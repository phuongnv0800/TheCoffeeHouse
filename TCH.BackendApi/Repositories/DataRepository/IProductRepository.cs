using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IProductRepository
{
    Task<Respond<PagedList<ProductVm>>> GetAllByBranchID(string branchID, Search request);

    Task<Respond<PagedList<ProductVm>>> GetAll(Search request);

    Task<Respond<PagedList<ProductVm>>> GetProductByCategoryID(string categoryID, Search request);

    Task<MessageResult> Create(ProductRequest request);

    Task<MessageResult> Delete(string productId);

    Task<MessageResult> Update(string productID, ProductRequest request);

    Task<MessageResult> CategoryAssign(string productID, string categoryID);

    Task<Respond<Product>> GetById(string productID);

    Task<MessageResult> AddImage(string productID, ProductImageRequest request);

    Task<Respond<PagedList<ProductImageVm>>> GetAllImages(string productID);

    Task<Respond<ProductImageVm>> GetImageById(string imageID);

    Task<MessageResult> RemoveImage(string imageID);

    Task<MessageResult> UpdateImage(string imageID, ProductImageRequest request);

    Task<Respond<PagedList<Size>>> GetAllSize();

    Task<MessageResult> UpdateSize(string sizeID, Size size);

    Task<MessageResult> CreateSize(Size size);

    Task<MessageResult> DeleteSize(string sizeID);

    Task<Respond<PagedList<Topping>>> GetAllTopping();

    Task<MessageResult> UpdateTopping(string toppingID, Topping topping);

    Task<MessageResult> CreateTopping(Topping topping);

    Task<MessageResult> DeleteTopping(string toppingID);
}
