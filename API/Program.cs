using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;
using Application.Services.Recipe;
using Application.Interfaces;
using OpenAIAPI;
using Infrastructure.AzureVaultService;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Application.Interfaces.Azure.Maps;
using Application.Services.AzureMaps;
using Application.Interfaces.Authentication.Helpers;
using Application.Services.Authentication.Helpers;
using Application.Repository.Authentication;
using Infrastructure.Repositories;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.Authentication;
using Application.Services.Authentication;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

// Add Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository and Services Registration
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthUserRepository, AuthUserRepository>();
builder.Services.AddScoped<IExternalLoginRepository, ExternalLoginRepository>();
builder.Services.AddScoped<IEntityCreationService, EntityCreationService>();
builder.Services.AddScoped<IUserEventRepository, UserEventRepository>();
builder.Services.AddSingleton<IRecipeParser, RecipeParser>();

// HttpClient Services
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddHttpClient<INearbySearchServiceAzureMaps, NearbySearchServiceAzureMaps>((services, client) =>
{
    var configuration = services.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["AzureMaps:BaseUrl"]);
});
builder.Services.AddScoped<IUserService, UserService>();


// Connect to Azure App Configuration
var appConfigUri = builder.Configuration.GetSection("Azure")["AppConfigurationUri"];
builder.Configuration.AddAzureAppConfiguration(options =>
{
    options.Connect(new Uri(appConfigUri), new ManagedIdentityCredential())
          .Select(KeyFilter.Any, LabelFilter.Null)
          .UseFeatureFlags();
});

// Connect to Azure Key Vault
var keyVaultUri = builder.Configuration.GetSection("Azure")["KeyVaultUri"];
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new ManagedIdentityCredential());

//Ef Core migration
if (Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DishIQ MVP Dev");
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DishIQ MVP");
});
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader().AllowAnyMethod();
});
app.MapControllers();
app.Run();
