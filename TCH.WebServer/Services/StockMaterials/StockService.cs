﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.StockMaterials
{
    public interface IStockService
    {
        Task<ResponseLogin<PagedList<StockMaterial>>> GetStockMaterials(bool IsPaging, int pageSize, int pageNumber, string name, string BranchId);
        Task DeleteAStockMaterial(string id);
    }
    public class StockService : IStockService
    {
        private readonly HttpClient httpClient;

        public StockService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task DeleteAStockMaterial(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Stocks/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<StockMaterial>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<StockMaterial>>>(content);
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

        public async Task<ResponseLogin<PagedList<StockMaterial>>> GetStockMaterials(bool IsPaging, int pageSize, int pageNumber, string name, string BranchId)
        {
            try
            {
                List<StockMaterial> Products;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<StockMaterial>>>($"/api/Stocks/{BranchId}?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + "&Name=" + name);
                if (response.Result != 1)
                {
                    return null;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}