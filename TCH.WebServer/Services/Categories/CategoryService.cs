﻿using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient httpClient;

        public CategoryService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<ResponseLogin<Category>> AddCategory(MultipartFormDataContent category)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Categories/", category);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Category> respond = JsonConvert.DeserializeObject<ResponseLogin<Category>>(content);
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

        public async Task DeleteCategory(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Categories/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Category>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Category>>>(content);
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

        public async Task<ResponseLogin<PagedList<Category>>> GetAllCategories()
        {
            var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Category>>>("/api/Categories");
            if (response.Result != 1)
            {
                return null;
            }
            return response;
        }

        public async Task<ResponseLogin<PagedList<Category>>> GetCategories(bool IsPaging, int pageSize, int pageNumber)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Category>>>("/api/Categories?IsPging=" + IsPaging.ToString()
                    + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString());
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

        public async Task<ResponseLogin<Category>> GetCategoryById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Categories/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Category> respond = JsonConvert.DeserializeObject<ResponseLogin<Category>>(content);
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

        public async Task<ResponseLogin<Category>> UpdateCategory(MultipartFormDataContent category)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Products/", category);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Category> respond = JsonConvert.DeserializeObject<ResponseLogin<Category>>(content);
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
