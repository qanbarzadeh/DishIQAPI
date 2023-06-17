using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;
using Application.Services.Recipe;
using Application.Interfaces;
using OpenAIAPI;
using Domain.AzureVault;
using Infrastructure.AzureVaultService;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Application.Interfaces.Azure.Maps;
using Application.Services.AzureMaps;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();

builder.Services.AddHttpClient<INearbySearchServiceAzureMaps, NearbySearchServiceAzureMaps>();


// Register RecipeService 
builder.Services.AddSingleton<IRecipeParser, RecipeParser>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
