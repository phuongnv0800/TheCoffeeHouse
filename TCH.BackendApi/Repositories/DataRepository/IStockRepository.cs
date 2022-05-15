using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IStockRepository
{
    Task<Respond<PagedList<StockVm>>> GetAllStockByBranchID(string branchID, Search request);
}
