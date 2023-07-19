using API;
using Application.Interfaces;
using Application.Interfaces.Authentication.Helpers;
using Application.Interfaces.Authentication.Manual;
using Application.Interfaces.Azure.Maps;
using Application.Interfaces.NutritionsAnalysis;
using Application.Interfaces.UnitOfWork;
using Application.Interfaces.UserRepo;
using Application.Mapping;
using Application.Services.Authentication.Helpers;
using Application.Services.Authentication.Manual;
using Application.Services.AzureMaps;
using Application.Services.OpenAI.ChatGptAPI;
using Application.Services.RecipenameSpace;
using Application.Services.UsersLinkRecipes;
using Azure.Identity;
using Domain.Entities.UserEntities;
using Infrastructure;
using Infrastructure.AzureVaultService;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using OpenAIAPI;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// AddIdentity services
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddDefaultTokenProviders();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Configure Authentication
builder.Services.ConfigureJwtAuthentication(builder.Configuration);

// Database Configuration
builder.Services.ConfigureDatabase(builder.Configuration);

// Repository and Services Registration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserResolverService, UserResolverService>();
builder.Services.AddScoped<ITokenService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var jwtSecret = config["JwtSecret"];
    return new TokenService(jwtSecret);
});

// Register repositories
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();
builder.Services.AddScoped<IRecipeIngredientRepository, RecipeIngredientRepository>();
builder.Services.AddScoped<Lazy<IRecipeIngredientRepository>>(x => new Lazy<IRecipeIngredientRepository>(() => x.GetRequiredService<IRecipeIngredientRepository>()));
builder.Services.AddScoped<IApplicationUserRepository, ApplicationUserRepository>();

builder.Services.AddScoped<Lazy<IRecipeRepository>>(x => new Lazy<IRecipeRepository>(() => x.GetRequiredService<IRecipeRepository>()));
builder.Services.AddScoped<Lazy<IApplicationUserRepository>>(x => new Lazy<IApplicationUserRepository>(() => x.GetRequiredService<IApplicationUserRepository>()));

// Added NutritionInformationRepository registration
builder.Services.AddScoped<INutritionInformationRepository, NutritionInformationRepository>();
builder.Services.AddScoped<Lazy<INutritionInformationRepository>>(x => new Lazy<INutritionInformationRepository>(() => x.GetRequiredService<INutritionInformationRepository>()));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();
builder.Services.AddSingleton<IRecipeParser, RecipeParser>();
builder.Services.AddScoped<IUserSpecificRecipeStorageService, UserSpecificRecipeStorageService>();

// HttpClient Services
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddHttpClient<INearbySearchServiceAzureMaps, NearbySearchServiceAzureMaps>((services, client) =>
{
    var configuration = services.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration["AzureMaps:BaseUrl"]);
});
builder.Services.AddScoped<IUserService, UserService>();

// Add SwaggerGen and configure endpoints
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DishIQ MVP Dev", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Enter 'Bearer' [space] and then your token in the text input below. Example: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();

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

builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "DishIQ MVP Dev");
        c.DefaultModelsExpandDepth(-1); // Disable the default model display
    });
}
else
{
    app.UseExceptionHandler("/error");
}

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});
app.MapControllers();
app.Run();
