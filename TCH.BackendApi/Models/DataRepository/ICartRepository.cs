using TCH.ViewModel.Catalog;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface ICartRepository
    {
        Task<bool> Create(CartRequest request);
        Task<bool> Delete(Guid userId, int productId);
        Task<IEnumerable<CartVm>> Get(Guid userId);
        Task<IEnumerable<CartVm>> GetByUserName(string userName);
    }
}