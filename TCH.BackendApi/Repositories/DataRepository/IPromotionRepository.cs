using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.SubModels;

namespace TCH.BackendApi.Repositories.DataManager
{
    public interface IPromotionRepository
    {
        Task<MessageResult> Create(PromotionRequest request);
        Task<MessageResult> Delete(string promotionID);
        Task<Respond<PagedList<Promotion>>> GetAll(Search request);
        Task<Respond<Promotion>> GetByCode(string code);
        Task<Respond<dynamic>> GetReduceMoney(string code, List<OrderItem> orderItems);
        Task<MessageResult> Update(string promotionID, PromotionRequest request);
    }
}