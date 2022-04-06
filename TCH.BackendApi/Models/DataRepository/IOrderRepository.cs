using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Enum;
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
        Task<Respond<PagedList<Order>>> GetByBranhID(string branhID, Search request);
        Task<Respond<PagedList<Order>>> GetAll(Search request);
        Task<Respond<Order>> GetById(string orderID);
        Task<MessageResult> UpdateStatus(string orderID, OrderStatus status);
        Task<Respond<PagedList<Order>>> GetByUser(string userID, Search request);
        Task<string> PrintInvoicePaymented(string invoiceID);
    }
}