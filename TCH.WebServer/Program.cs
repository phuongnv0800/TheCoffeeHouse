﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using TCH.WebServer.Services;
using TCH.WebServer.Services.Products;
using TCH.WebServer.Services.Categories;
using TCH.WebServer.Services.Users;
using TCH.WebServer.Services.Brands;
using TCH.WebServer.Services.Materials;
using TCH.WebServer.Services.OrderDetails;
using TCH.WebServer.Services.Orders;
using TCH.WebServer.Services.Promotions;
using TCH.WebServer.Services.Reports;
using VMU.Components;
using TCH.WebServer.Services.StockMaterials;
using TCH.WebServer.Services.Units;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Configuration["BackendApiUrl"]) });
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpClient();
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IBrandService, BrandService>();
builder.Services.AddTransient<IMaterialService, MaterialService>();
builder.Services.AddTransient<IOrderDetailService, OrderDetailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IReportService, ReportService>();
builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddTransient<IUnitService, UnitService>();

builder.Services.AddScoped<IUserApiClient, UserApiClient>();
builder.Services.AddScoped<IRoleApiClient, RoleApiClient>();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddServices();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
