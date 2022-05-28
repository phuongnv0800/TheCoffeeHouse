using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Reports
{
    public interface IReportService
    {
        Task<ResponseLogin<PagedList<Report>>> GetAllImport(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<PagedList<Report>>> GetAllExport(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<PagedList<Report>>> GetAllLiquid(bool IsPaging, int pageSize, int pageNumber);
        Task<ResponseLogin<PagedList<Report>>> GetAllImportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId);
        Task<ResponseLogin<PagedList<Report>>> GetAllExportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId);
        Task<ResponseLogin<PagedList<Report>>> GetAllLiquidInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId);
        Task<ResponseLogin<Report>> AddImport(ImportRequest Promotion);
        Task<ResponseLogin<Report>> AddExport(ExportRequest Promotion);
        Task<ResponseLogin<Report>> AddLiquid(ExportRequest Promotion);
        Task<ResponseLogin<Report>> GetImportnById(string id);
        Task<ResponseLogin<Report>> GetExportnById(string id);
        Task<ResponseLogin<Report>> GetLiquidById(string id);
        Task DeleteImport(string id);
        Task DeleteExport(string id);
        Task DeleteLiquid(string id);
    }
    public class ReportService : IReportService
    {
        private readonly HttpClient httpClient;

        public ReportService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseLogin<Report>> AddExport(ExportRequest Promotion)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Promotion), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Reports/export", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Report> respond = JsonConvert.DeserializeObject<ResponseLogin<Report>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<Report>> AddImport(ImportRequest Promotion)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Promotion), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Reports/import", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Report> respond = JsonConvert.DeserializeObject<ResponseLogin<Report>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<Report>> AddLiquid(ExportRequest Promotion)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Promotion), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Reports/liquidation", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Report> respond = JsonConvert.DeserializeObject<ResponseLogin<Report>>(content);
                    if (respond.Result == 1)
                    {
                        return respond;
                    }
                    return null;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteExport(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Reports/export/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Report>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Report>>>(content);
                    if (respond.Result == 1)
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteImport(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Reports/import/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Report>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Report>>>(content);
                    if (respond.Result == 1)
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleteLiquid(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Reports/liquidation/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Report>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Report>>>(content);
                    if (respond.Result == 1)
                    {
                        return;
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllExport(bool IsPaging, int pageSize, int pageNumber)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/export?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllExportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/export/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllImport(bool IsPaging, int pageSize, int pageNumber)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/import?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllImportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/import/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllLiquid(bool IsPaging, int pageSize, int pageNumber)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/liquidation?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllLiquidInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/liquidation/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public Task<ResponseLogin<Report>> GetExportnById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseLogin<Report>> GetImportnById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseLogin<Report>> GetLiquidById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
