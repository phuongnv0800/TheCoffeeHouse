using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IStockRepository
{
    Task<Respond<PagedList<StockMaterial>>> GetAllStockByBranchID(string branchID, Search request);
}
