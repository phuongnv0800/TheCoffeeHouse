using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Reports
{
    public interface IReportService
    {
        Task<ResponseLogin<PagedList<Report>>> GetAllImport(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<Report>>> GetAllExport(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<Report>>> GetAllLiquid(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<Report>>> GetAllImportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<Report>>> GetAllExportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<Report>>> GetAllLiquidInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<Report>> AddImport(ImportRequest Promotion);
        Task<ResponseLogin<Report>> AddExport(ExportRequest Promotion);
        Task<ResponseLogin<Report>> AddLiquid(ExportRequest Promotion);
        Task<ResponseLogin<Report>> GetImportnById(string id);
        Task<ResponseLogin<Report>> GetExportnById(string id);
        Task<ResponseLogin<Report>> GetLiquidById(string id);
        Task<Byte[]> ExcelExport(DateTime? FromDate, DateTime? ToDate);
        Task<Byte[]> ExcelImport(DateTime? FromDate, DateTime? ToDate);
        Task<Byte[]> ExcelLiquid(DateTime? FromDate, DateTime? ToDate);
        Task<Byte[]> ExcelExportInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<Byte[]> ExcelImportInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<Byte[]> ExcelLiquidInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<MassMaterial>>> GetMassMaterialInDay(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<PagedList<MassMaterial>>> GetMassMaterialInDayByBranchId(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate);
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

        public async Task<byte[]> ExcelExport(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-export-all?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> ExcelExportInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-export/{BranchId}?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> ExcelImport(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-import-all?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> ExcelImportInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-import/{BranchId}?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> ExcelLiquid(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-liquidation-all?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> ExcelLiquidInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Reports/excel-liquidation/{BranchId}?" + fromDate
                        + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllExport(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/export?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate + toDate);
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllExportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/export-by-branch-id/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() +fromDate + toDate);
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllImport(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/import?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate + toDate);
            
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllImportInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/import-by-branch-id/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate +toDate);
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllLiquid(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>("/api/Reports/liquidation?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate +toDate);
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Report>>> GetAllLiquidInBranch(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Report>>>($"/api/Reports/liquidation-by-branch-id/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate +toDate);
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<Report>> GetExportnById(string id)
        {
            
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<Report>>($"/api/Reports/export/{id}");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<Report>> GetImportnById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<Report>>($"/api/Reports/import/{id}");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<Report>> GetLiquidById(string id)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<Report>>($"/api/Reports/liquid/{id}");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<MassMaterial>>> GetMassMaterialInDay(bool IsPaging, int pageSize, int pageNumber, DateTime? FromDate, DateTime? ToDate) 
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<MassMaterial>>>("/api/Reports/get-mass-material-in-day?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate + toDate);
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<MassMaterial>>> GetMassMaterialInDayByBranchId(bool IsPaging, int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<MassMaterial>>>($"/api/Reports/get-mass-material-in-day-by-branch/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate + toDate);
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }
    }
}
