using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IBranchRepository
{
    Task<MessageResult> AddUserToBranch(string userID, string branchID);
    Task<MessageResult> Create(BranchRequest request);
    Task<MessageResult> Delete(string id);
    Task<Respond<PagedList<Branch>>> GetAll(Search request);
    Task<MessageResult> RemoveUserToBranch(string userID, string branchID);
    Task<MessageResult> Update(string id, BranchRequest request);
    Task<Respond<Branch>> GetByID(string branchID);
}
