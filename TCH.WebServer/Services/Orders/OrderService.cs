﻿using TCH.Utilities.Paginations;
using TCH.WebServer.Models;
using TCH.Data.Entities;
using TCH.ViewModel.SubModels;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;

namespace TCH.WebServer.Services.Orders
{
    public interface IOrderService
    {
        Task<ResponseLogin<PagedList<Order>>> GetAllOrders(string orderId);
        Task<ResponseLogin<Order>> AddOrder(OrderRequest branch);
        Task<ResponseLogin<Order>> GetOrderById(string id);
        Task<ResponseLogin<Order>> UpdateOrder(OrderRequest branch);
        Task DeleteOrder(string id);
    }
    public class OrderService : IOrderService
    {
        private readonly HttpClient httpClient;

        public OrderService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Order>> AddOrder(OrderRequest order)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Orders/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
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

        public async Task DeleteOrder(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Orders/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Order>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Order>>>(content);
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
        public async Task<ResponseLogin<Order>> UpdateOrder(OrderRequest Order)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(Order), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Branchs/", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
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
        public async Task<ResponseLogin<PagedList<Order>>> GetAllOrders(string orderId)
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Order>>>($"/api/Orders/{orderId}");
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
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Orders/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
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
    }
}