using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TCH.Web;
using TCH.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
//builder.Services.AddHttpClient();
//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
////builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://7c94-125-212-156-93.ngrok.io/") });
//builder.Services.AddScoped<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:8000/") });

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddTransient<IAuthService, AuthService>();

await builder.Build().RunAsync();
