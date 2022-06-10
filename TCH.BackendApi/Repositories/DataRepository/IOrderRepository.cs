using TCH.Data.Entities;
using TCH.Utilities.Enum;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.ViewModel.SubModels;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IOrderRepository
{
    Task<Respond<object>> Create(OrderRequest request);
    Task<MessageResult> Update(OrderRequest request);
    Task<Respond<object>> GetAllMoney(Search request);
    Task<Respond<object>> GetAllMoneyByBranchId(string branhID, Search request);
    Task<MessageResult> Delete(string orderID);
    Task<Respond<PagedList<Order>>> GetByBranhID(string branhID, Search request);
    Task<Respond<PagedList<Order>>> GetAll(Search request);
    Task<Respond<Order>> GetById(string orderID);
    Task<MessageResult> UpdateStatus(string orderID, OrderStatus status);
    Task<Respond<PagedList<Order>>> GetByUser(string userID, Search request);
    Task<string> PrintInvoicePaymented(string invoiceID);
    Task<string> ExcelAllOrder(Search request);
    Task<string> ExcelAllOrderByBranchID(string branchId, Search request);
    Task<Respond<PagedList<ProductQuantityVm>>> GetProductInOrderAllBranch(string productId, Search search);
    Task<Respond<PagedList<ProductQuantityVm>>> GetProductInOrder(string? branchId, string productId, Search search);
}