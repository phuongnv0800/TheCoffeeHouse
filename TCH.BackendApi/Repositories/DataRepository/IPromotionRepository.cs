using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataRepository
{
    public interface IPromotionRepository
    {
        Task<MessageResult> Create(PromotionRequest request);
        Task<MessageResult> Delete(string promotionID);
        Task<MessageResult> DeleteByCode(string code);
        Task<Respond<PagedList<Promotion>>> GetAll(Search request);
        Task<Respond<Promotion>> GetByCode(string code);
        Task<Respond<dynamic>> GetReduceMoney(string code, List<OrderItem> orderItems);
        Task<MessageResult> Update(string promotionID, PromotionRequest request);
    }
}