using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IStockRepository
{
    Task<MessageResult> CreateStockMaterial(StockRequest request);
    Task<MessageResult> DeleteStockMaterial(string branchID, string materialID);
    Task<Respond<PagedList<StockVm>>> GetAllStock(Search request);
    Task<Respond<PagedList<StockVm>>> GetAllStockByBranchID(string branchID, Search request);
    Task<MessageResult> UpdateStockMaterial(StockRequest request);
}
