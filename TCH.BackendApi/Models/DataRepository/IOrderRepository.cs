using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.ViewModels;
using TCH.BackendApi.Models.SubModels;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IOrderRepository
    {
        Task<MessageResult> Create(OrderRequest request);
        Task<MessageResult> Update(OrderRequest request);
        Task<MessageResult> Delete(string orderID);
        Task<Respond<PagedList<OrderVm>>> GetByBranhID(string branhID, Search request);
        Task<Respond<PagedList<OrderVm>>> GetAll(Search request);
        Task<OrderVm> GetById(string orderID);
        Task<Respond<PagedList<OrderVm>>> GetByUser(Guid userID, Search request);
    }
}