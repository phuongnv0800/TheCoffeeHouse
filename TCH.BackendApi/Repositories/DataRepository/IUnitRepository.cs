using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IUnitRepository
{
    Task<MessageResult> Create(UnitRequest request);
    Task<MessageResult> CreateExchangeUnit(ExchangeUnitRequest request);
    Task<MessageResult> Delete(string id);
    Task<Respond<PagedList<MeasuresVm>>> GetAll(Search request);
    Task<Respond<List<UnitConversion>>> GetAllExchangeUnit();
    Task<MessageResult> Update(string id, UnitRequest request);
    Task<MessageResult> UpdateExchangeUnit(ExchangeUnitRequest request);
}
