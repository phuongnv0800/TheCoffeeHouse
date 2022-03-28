using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository;

public interface IMenuRepository
{
    Task<MessageResult> ActiveProductInMenu(string menuID, string productID);
    Task<MessageResult> Create(string branchID, MenuRequest request);
    Task<MessageResult> DeactiveProductInMenu(string menuID, string productID);
    Task<MessageResult> Delete(string id);
    void Dispose();
    Task<Respond<List<Menu>>> GetMenu(string branchID);
    Task<MessageResult> Update(string menuID, MenuRequest request);
}
