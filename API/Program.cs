using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;
using Application.Services.Recipe;
using Application.Interfaces;
using OpenAIAPI;
using Application.Services.SelectionAndOrder;
using Domain.AzureVault;
using Infrastructure.AzureVaultService;
using Azure.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Azure.Security.KeyVault.Secrets;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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
var keyVaultUri = builder.Configuration.GetSection("Azure")["KeyVaultUri"];
builder.Configuration.AddAzureKeyVault(new Uri(keyVaultUri), new DefaultAzureCredential());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
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
