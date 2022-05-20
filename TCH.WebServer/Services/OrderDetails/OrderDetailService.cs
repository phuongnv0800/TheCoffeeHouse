using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.OrderDetails
{
    public interface IOrderDetailService
    {
        Task<ResponseLogin<PagedList<OrderDetail>>> GetAllOrderDetails(string orderId);
        Task<ResponseLogin<OrderDetail>> AddOrderDetail(OrderItem branch);
        Task<ResponseLogin<OrderDetail>> GetOrderDetailById(string id);
        Task<ResponseLogin<OrderDetail>> UpdateOrderDetail(OrderItem branch);
        Task DeleteOrderDetail(string id);
    }
    public class OrderDetailService : IOrderDetailService
    {
        private readonly HttpClient httpClient;

        public OrderDetailService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<OrderDetail>> AddOrderDetail(OrderItem orderDetail)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(orderDetail), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/OrderDetails/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<OrderDetail> respond = JsonConvert.DeserializeObject<ResponseLogin<OrderDetail>>(content);
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

        public async Task DeleteOrderDetail(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/OrderDetails/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<OrderDetail>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<OrderDetail>>>(content);
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
        public async Task<ResponseLogin<OrderDetail>> UpdateOrderDetail(OrderItem orderDetail)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(orderDetail), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Branchs/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<OrderDetail> respond = JsonConvert.DeserializeObject<ResponseLogin<OrderDetail>>(content);
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
        public async Task<ResponseLogin<PagedList<OrderDetail>>> GetAllOrderDetails(string orderId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<OrderDetail>>>($"/api/OrderDetails/{orderId}");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }
        public async Task<ResponseLogin<OrderDetail>> GetOrderDetailById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/OrderDetails/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<OrderDetail> respond = JsonConvert.DeserializeObject<ResponseLogin<OrderDetail>>(content);
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
