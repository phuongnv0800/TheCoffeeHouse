using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.ViewModel.Catalog;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IOrderRepository
    {
        Task<bool> Create(OrderRequest request);
        Task<PagedList<OrderVm>> GetAllPaging(Search request);
        Task<OrderVm> GetById(int orderId);
        Task<PagedList<OrderVm>> GetByUser(Guid userId, Search request);
        Task<bool> Update(OrderRequest request);
    }
}