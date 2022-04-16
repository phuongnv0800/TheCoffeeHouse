using TCH.Data.Entities;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IMenuRepository
{
    Task<MessageResult> ActiveProductInMenu(string menuID, string productID);
    Task<MessageResult> Create(string branchID, MenuRequest request);
    Task<MessageResult> DeactiveProductInMenu(string menuID, string productID);
    Task<MessageResult> Delete(string id);
    Task<Respond<List<Menu>>> GetMenu(string branchID);
    Task<MessageResult> Update(string menuID, MenuRequest request);
}
