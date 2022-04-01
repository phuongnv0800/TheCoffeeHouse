using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TCH.BackendApi.EF;
using TCH.BackendApi.Entities;
using TCH.BackendApi.Models.DataManager;
using TCH.BackendApi.Models.DataRepository;
using TCH.BackendApi.ViewModels;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager config = builder.Configuration;
IWebHostEnvironment environment = builder.Environment;

builder.Services.AddResponseCaching();

builder.Services.AddDistributedMemoryCache();
// Add services to the container.
builder.Services.AddDbContext<APIContext>(options =>
{
    options.UseLoggerFactory(LoggerFactory.Create(builder => { builder.AddConsole(); }));
    options.EnableSensitiveDataLogging();
    //if (environment.IsProduction())
    //{
    //    options.UseSqlServer(config.GetConnectionString("Prod"));
    //}
    //else
    //{
    //    options.UseSqlServer(config.GetConnectionString("Dev"));
    //}
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(c => c.AddProfile<AutoMapping>(), typeof(Program));
builder.Services.AddIdentity<AppUser, AppRole>()
    .AddEntityFrameworkStores<APIContext>()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<IProductRepository, ProductManager>();
builder.Services.AddScoped<ICategoryRepository, CategoryManager>();
builder.Services.AddScoped<IUserRepository, UserManager>();
builder.Services.AddScoped<IRoleRepository, RoleManager>();
builder.Services.AddScoped<IMaterialRepository, MaterialManager>();
builder.Services.AddScoped<IBranchRepository, BranchManager>();
builder.Services.AddScoped<IMenuRepository, MenuManager>();
//builder.Services.AddScoped<IOrderRepository, OrderManager>();

builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IStorageService, FileStorageService>();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

//services.AddAutoMapper(typeof(ServiceCollectionExtensions));
//services.AddMediatR(typeof(ServiceCollectionExtensions));
//services.AddServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TCH.BackendApi", Version = "v1" });
    //authorize
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"3WT Authorization header using the Bearer scheme,
                        \r\n\r\n Enter 'Bearer'[space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer",
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                    },
                    new List<string>()
                    }
                });
});
//jwt
string issuer = config.GetValue<string>("Jwt:Issuer");
string signingKey = config.GetValue<string>("Jwt:Key");
byte[] signingKeyBytes = Encoding.UTF8.GetBytes(signingKey);

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = issuer,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = System.TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(signingKeyBytes)
    };
});
builder.Services.AddCors(option =>
{
    option.AddPolicy("CorsPolicy", builder => builder
        .SetIsOriginAllowed(origin => true)
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});
var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<APIContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB!..");
    }
}
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseResponseCaching();
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
           Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
    RequestPath = "/uploads"
});

app.UseCors("CorsPolicy");
app.UseRouting();


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
