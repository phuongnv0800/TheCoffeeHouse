using TCH.Utilities.Paginations;
using TCH.WebServer.Models;
using TCH.Data.Entities;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Globalization;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.WebUtilities;
using TCH.Utilities.Searchs;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;

namespace TCH.WebServer.Services.Orders
{
    public interface IOrderService
    {
        Task<ResponseLogin<PagedList<Order>>> GetAllOrders(bool IsPaging, int pageSize, int pageNumber,
            DateTime? FromDate, DateTime? ToDate);

        Task<ResponseLogin<PagedList<Order>>> GetAllOrdersInBranch(bool IsPaging, int pageSize, int pageNumber,
            string BranchId, string id, DateTime? FromDate, DateTime? ToDate);

        Task<Respond<PagedList<Order>>> GetOrderByUser(bool IsPaging, int pageSize, int pageNumber,
            string BranchId, string userId, DateTime? FromDate, DateTime? ToDate);

        Task<ResponseLogin<Order>> AddOrder(OrderRequest branch);
        Task<ResponseLogin<Order>> GetOrderById(string id);

        Task<ResponseLogin<object>> GetSum(DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<object>> GetSumInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<byte[]> GetExcel(DateTime? FromDate, DateTime? ToDate);
        Task<byte[]> GetExcelInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate);
        Task<ResponseLogin<Order>> UpdateOrder(OrderRequest branch);

        Task<ResponseLogin<PagedList<ProductQuantityVm>>> GetQuantiProducts(bool IsPaging, int pageSize, int pageNumber,
            DateTime? FromDate, DateTime? ToDate);

        Task<ResponseLogin<PagedList<ProductQuantityVm>>> GetQuantiProductsByBranch(bool IsPaging, int pageSize,
            int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate);

        Task<Respond<PagedList<OrderInUser>>> GetOrderByCompareUser(bool IsPaging, int pageSize,
            int pageNumber, string BranchId, string name, DateTime? FromDate, DateTime? ToDate);
        Task<string> PrintPDF(string id);
        Task DeleteOrder(string id);

        Task<Respond<MoneyByDay>> GetChartMoney(DateTime? FromDate, DateTime? ToDate);
        Task<Respond<MoneyByDay>> GetChartMoneyByBranchId(string BranchId, DateTime? FromDate, DateTime? ToDate);
    }

    public class OrderService : BaseApiClient, IOrderService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;

        public OrderService(HttpClient httpClient, ILocalStorageService localStorage)
            : base(httpClient, localStorage)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
        }

        public async Task<Respond<PagedList<Order>>> GetOrderByUser(bool IsPaging, int pageSize, int pageNumber,
            string BranchId, string userId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                var token = await localStorage.GetItemAsync<string>("authToken");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetFromJsonAsync<Respond<PagedList<Order>>>($"/api/Orders/user/{userId}?IsPging=" 
                    + IsPaging
                    + "&PageNumber=" + pageNumber + "&PageSize=" + pageSize + fromDate + toDate);
                return response;
            }
            catch (Exception e)
            {
                return new Respond<PagedList<Order>>()
                {
                    Data = null,
                    Result = 0,
                    Message = "",
                };
            }
        }

        public async Task<ResponseLogin<Order>> AddOrder(OrderRequest order)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent =
                    new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Orders/", httpContent);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Order> respond = JsonConvert.DeserializeObject<ResponseLogin<Order>>(content);
                    return respond;
                }

                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task DeleteOrder(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Orders/{id}");
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Order>> respond =
                        JsonConvert.DeserializeObject<ResponseLogin<PagedList<Order>>>(content);
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

        public async Task<Respond<MoneyByDay>> GetChartMoney(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.GetDateTimeFormats()[0] : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.GetDateTimeFormats()[0] : "";
                var response =
                    await httpClient.GetFromJsonAsync<Respond<MoneyByDay>>($"/api/Orders/get-revenue?"
                                                                           + fromDate + toDate);
                return response;
            }
            catch (Exception e)
            {
                throw;
                return new Respond<MoneyByDay>();
            }
        }

        public async Task<Respond<MoneyByDay>> GetChartMoneyByBranchId(string BranchId, DateTime? FromDate,
            DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.GetDateTimeFormats()[0] : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.GetDateTimeFormats()[0] : "";
                var response = await httpClient.GetFromJsonAsync<Respond<MoneyByDay>>(
                    $"/api/Orders/get-revenue-branch/{BranchId}?" + fromDate + toDate);
                return response;
            }
            catch
            {
                throw;
                return new Respond<MoneyByDay>();
            }
        }

        public async Task<ResponseLogin<Order>> UpdateOrder(OrderRequest Order)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent =
                    new StringContent(JsonConvert.SerializeObject(Order), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Branchs/", httpContent);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Order> respond = JsonConvert.DeserializeObject<ResponseLogin<Order>>(content);
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

        public async Task<ResponseLogin<PagedList<Order>>> GetAllOrders(bool IsPaging, int pageSize, int pageNumber,
            DateTime? FromDate, DateTime? ToDate)
        {
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
            string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Order>>>($"/api/Orders/?IsPging=" +
                IsPaging.ToString()
                + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + fromDate
                + toDate);
            if (response.Result != 1)
            {
                return null;
            }

            return response;
        }

        public async Task<ResponseLogin<Order>> GetOrderById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Orders/{id}");
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Order> respond = JsonConvert.DeserializeObject<ResponseLogin<Order>>(content);
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

        public async Task<string> PrintPDF(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Orders/print/{id}");
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<ResponseLogin<PagedList<Order>>> GetAllOrdersInBranch(bool IsPaging, int pageSize,
            int pageNumber, string BranchId, string id, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Order>>>(
                    $"/api/Orders/branch/{BranchId}?IsPging=" + IsPaging.ToString()
                                                              + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" +
                                                              pageSize.ToString() + "&BranchId=" + BranchId
                                                              + fromDate
                                                              + toDate);
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

        public async Task<ResponseLogin<object>> GetSum(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/all-money?" + fromDate
                    + toDate);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<object> respond = JsonConvert.DeserializeObject<ResponseLogin<object>>(content);
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

        public async Task<ResponseLogin<object>> GetSumInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/all-money/{BranchId}?" + fromDate
                    + toDate);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<object> respond = JsonConvert.DeserializeObject<ResponseLogin<object>>(content);
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

        public async Task<byte[]> GetExcel(DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/excel-all?" + fromDate
                    + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<byte[]> GetExcelInBranch(string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/excel-by-branchID/{BranchId}?" + fromDate
                    + toDate);
                return await response.Content.ReadAsByteArrayAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<ResponseLogin<PagedList<ProductQuantityVm>>> GetQuantiProducts(bool IsPaging, int pageSize,
            int pageNumber, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/get-product-branch-all?IsPging=" +
                                                         IsPaging.ToString()
                                                         + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" +
                                                         pageSize.ToString() + fromDate
                                                         + toDate);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<ProductQuantityVm>> respond =
                        JsonConvert.DeserializeObject<ResponseLogin<PagedList<ProductQuantityVm>>>(content);
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

        public async Task<ResponseLogin<PagedList<ProductQuantityVm>>> GetQuantiProductsByBranch(bool IsPaging,
            int pageSize, int pageNumber, string BranchId, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/get-product-branch/{BranchId}?IsPging=" +
                                                         IsPaging.ToString()
                                                         + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" +
                                                         pageSize.ToString() + fromDate
                                                         + toDate);
                if ((int) response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<ProductQuantityVm>> respond =
                        JsonConvert.DeserializeObject<ResponseLogin<PagedList<ProductQuantityVm>>>(content);
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

        public async Task<Respond<PagedList<OrderInUser>>> GetOrderByCompareUser(bool IsPaging, int pageSize, int pageNumber, string BranchId, string name, DateTime? FromDate, DateTime? ToDate)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                string fromDate = FromDate != null ? "&StartDate=" + FromDate.Value.ToShortDateString() : "";
                string toDate = ToDate != null ? "&EndDate=" + ToDate.Value.ToShortDateString() : "";
                var response = await httpClient.GetAsync($"/api/Orders/compare/{BranchId}?IsPging=" +
                                                         IsPaging.ToString()
                                                         + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" +
                                                         pageSize.ToString() + fromDate
                                                         + toDate);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Respond<PagedList<OrderInUser>> respond =
                        JsonConvert.DeserializeObject<Respond<PagedList<OrderInUser>>>(content);
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
    }
}