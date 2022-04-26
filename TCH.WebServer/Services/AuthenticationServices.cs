using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using TCH.WebServer.Models;

namespace TCH.WebServer.Services
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private NavigationManager _navigationManager;
        private readonly IHttpClientFactory clientFactory;
        public AuthenticationServices(
            NavigationManager navigationManager, IHttpClientFactory clientFactory
        )
        {
            _navigationManager = navigationManager;
            this.clientFactory = clientFactory;
        }
        //aaa
        public async Task<ResponseLogin<string>> Login(RequestLogin requestLogin)
        {
            try
            {
                string host = "http://a5e2-125-212-156-93.ngrok.io/api/Users/authenticate";
                var client = clientFactory.CreateClient();
                var request = new HttpRequestMessage(HttpMethod.Post, host);
                var httpContent = new StringContent(JsonConvert.SerializeObject(requestLogin), Encoding.UTF8, "application/json");
                var response = await client.PostAsync(host, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    ResponseLogin<string> respond = JsonConvert.DeserializeObject<ResponseLogin<string>>(content);
                    return respond;
                }
                return new ResponseLogin<string> { Result = -1, Message = response.ToString(), Data = "" };
            }
            catch (Exception ex)
            {
                return new ResponseLogin<string>(){ Result = -1, Message = ex.ToString() };
            }
        }

        public async Task Logout()
        {
            _navigationManager.NavigateTo("login");
        }
    }
}
