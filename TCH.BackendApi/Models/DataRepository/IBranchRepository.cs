using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.Paginations;
using TCH.BackendApi.Models.Searchs;
using TCH.BackendApi.Models.SubModels;
using TCH.BackendApi.ViewModels;

namespace TCH.BackendApi.Models.DataRepository
{
    public interface IBranchRepository
    {
        Task<MessageResult> AddUserToBranch(string userID, string branchID);
        Task<MessageResult> Create(BranchRequest request);
        Task<MessageResult> Delete(string id);
        void Dispose();
        Task<Respond<PagedList<Branch>>> GetAll(Search request);
        Task<MessageResult> RemoveUserToBranch(string userID, string branchID);
        Task<MessageResult> Update(string id, BranchRequest request);
        Task<Respond<Branch>> GetByID(string branchID);
    }
}