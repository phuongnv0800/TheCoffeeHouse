using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Units
{
    public interface IUnitService
    {
        Task<ResponseLogin<PagedList<MeasuresVm>>> GetAllMaterials();
    }
    public class UnitService : IUnitService
    {
        private readonly HttpClient httpClient;

        public UnitService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseLogin<PagedList<MeasuresVm>>> GetAllMaterials()
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<MeasuresVm>>>("/api/Units");
                if (response.Result != 1)
                {
                    return null;
                }
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
