using Newtonsoft.Json;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.Utilities.SubModels;
using TCH.ViewModel.RequestModel;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Customers
{
    public interface ICustomerService
    {
        public Task<ResponseLogin<PagedList<Customer>>> GetAllCustomer(bool IsPaging, int pageSize, int pageNumber, string sdt);
        public Task<ResponseLogin<PagedList<Bean>>> GetAllCustomerType(bool IsPaging, int pageSize, int pageNumber, string name);
        public Task<ResponseLogin<Customer>> GetAllCustomerByPhone(string sdt);
        public Task<MessageResult> AddCustomer(MultipartFormDataContent request);
        public Task<MessageResult> UpdateCustomer(MultipartFormDataContent request, string id);
        public Task<ResponseLogin<Customer>> GetCustomerById(string id);
        public Task<Respond<Customer>> GetPromotion(string customerId);
        Task<Respond<Customer>> ExchangePoint(string customerId, string promotionId);
        Task<MessageResult> CreateMemberType(MemberTypeRequest request);
        Task<MessageResult> DeleteMemberType(string id);
        Task<MessageResult> UpdateMemberType(string id, MemberTypeRequest request);
        public Task DeleCustomer(string id);
    }
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<MessageResult> AddCustomer(MultipartFormDataContent request)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Customers/", request);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageResult respond = JsonConvert.DeserializeObject<MessageResult>(content);
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
        public async Task<Respond<Customer>> ExchangePoint(string customerId, string promotionId)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync($"/api/Customers/exchange/{customerId}", promotionId);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Respond<Customer> respond = JsonConvert.DeserializeObject<Respond<Customer>>(content);
                    return respond;
                }
                return null;
            }
            catch
            {
                throw;
            }
        }
        public async Task<Respond<Customer>> GetPromotion(string customerId)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<Respond<Customer>>($"/api/Customers/promotions/{customerId}");
                return response;
            }
            catch
            {
                throw;
            }
        }

        public async Task DeleCustomer(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Customers/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<PagedList<Customer>> respond = JsonConvert.DeserializeObject<ResponseLogin<PagedList<Customer>>>(content);
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

        public async Task<ResponseLogin<PagedList<Customer>>> GetAllCustomer(bool IsPaging, int pageSize, int pageNumber, string sdt)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Customer>>>($"/api/Customers/?IsPging=" + IsPaging.ToString()
                        + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + "&Name=" + sdt);
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

        public async Task<ResponseLogin<Customer>> GetAllCustomerByPhone(string sdt)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<Customer>>($"/api/Customers/phone-number/{sdt}");
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

        public async Task<ResponseLogin<Customer>> GetCustomerById(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.GetAsync($"/api/Customers/id/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<Customer> respond = JsonConvert.DeserializeObject<ResponseLogin<Customer>>(content);
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

        public async Task<MessageResult> UpdateCustomer(MultipartFormDataContent request, string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                //var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Customers/{id}", request);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageResult respond = JsonConvert.DeserializeObject<MessageResult>(content);
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

        public async Task<ResponseLogin<PagedList<Bean>>> GetAllCustomerType(bool IsPaging, int pageSize, int pageNumber, string name)
        {
            try
            {
                var response = await httpClient.GetFromJsonAsync<ResponseLogin<PagedList<Bean>>>($"/api/Customers/member-type/?IsPging=" + IsPaging.ToString()
                        + "&PageNumber=" + pageNumber.ToString() + "&PageSize=" + pageSize.ToString() + "&Name=" + name);
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

        public async Task<MessageResult> CreateMemberType(MemberTypeRequest request)
        {
            try
            {
                CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Customers/member-type", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageResult respond = JsonConvert.DeserializeObject<MessageResult>(content);
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

        public async Task<MessageResult> DeleteMemberType(string id)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var response = await httpClient.DeleteAsync($"/api/Customers/member-type/{id}");
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageResult respond = JsonConvert.DeserializeObject<MessageResult>(content);
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

        public async Task<MessageResult> UpdateMemberType(string id, MemberTypeRequest request)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PutAsync($"/api/Customers/member-type/{id}", httpContent);
                if ((int)response.StatusCode == StatusCodes.Status200OK)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    MessageResult respond = JsonConvert.DeserializeObject<MessageResult>(content);
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
