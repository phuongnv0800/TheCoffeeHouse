using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using TCH.Data.Entities;
using TCH.Utilities.Paginations;
using TCH.ViewModel.SubModels;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services.Customers
{
    public interface ICustomerService
    {
        public Task<ResponseLogin<PagedList<Customer>>> GetAllCustomer(bool IsPaging, int pageSize, int pageNumber, string sdt);
        public Task<ResponseLogin<Customer>> AddCustomer(CustomerRequest request);
    }
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient httpClient;

        public CustomerService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<ResponseLogin<Customer>> AddCustomer(CustomerRequest request)
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GbParameter.GbParameter.Token);
                var httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync($"/api/Customers/", httpContent);
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
    }
}
