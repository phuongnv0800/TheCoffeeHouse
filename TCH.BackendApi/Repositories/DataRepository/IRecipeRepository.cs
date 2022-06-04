using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;

namespace TCH.BackendApi.Repositories.DataRepository;

public interface IRecipeRepository
{
    Task<MessageResult> Create(IEnumerable<RecipeRequest> request);
    Task<MessageResult> Delete(string productId);
    Task<Respond<PagedList<RecipeDetail>>> GetAll(Search request);
    Task<Respond<IEnumerable<RecipeDetail>>> GetRecipeByProductSize(string productID, string sizeID);
    Task<Respond<IEnumerable<RecipeDetail>>> GetRecipeByProductID(string productID);
    Task<MessageResult> Update(RecipeRequest request);
}
