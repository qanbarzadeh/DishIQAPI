using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Application.Mapping;
using Application.Services.Recipe;
using Application.Configuration;
using Application.Interfaces;
using OpenAIAPI;
using Application.Services.SelectionAndOrder;
using Domain.AzureVault;
using Infrastructure.AzureVaultService;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container..
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();
builder.Services.AddScoped<IIngredientsSelectionService, IngredientsSelectionService>();
builder.Services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();

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
          .UseFeatureFlags()
          .ConfigureRefresh(refresh =>
          {
              refresh.Register("TestApp:Settings", refreshAll: true)
                     .SetCacheExpiration(TimeSpan.FromMinutes(5));
          });
});

// Connect to Azure Key Vault
var azureServiceTokenProvider = new AzureServiceTokenProvider();
var keyVaultClient = new KeyVaultClient(
     new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
var keyVaultUri = builder.Configuration.GetSection("Azure")["KeyVaultUri"];

builder.Configuration.AddAzureKeyVault(
     vault: keyVaultUri,
     client: keyVaultClient,
     manager: new DefaultKeyVaultSecretManager());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader().AllowAnyMethod();
});

app.MapControllers();

app.Run();
