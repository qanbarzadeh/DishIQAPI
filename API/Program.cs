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
using Microsoft.Identity.Web;
using Application.Interfaces.Azure.Maps;
using Application.Services.AzureMaps;
using Application.Interfaces.Authentication.Helpers;
using Application.Services.Authentication.Helpers;
using Application.Interfaces.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Application.Interfaces.Authentication.Manual;
using Application.Services.Authentication.Manual;
using Application.Repository.Authentication;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// AddIdentity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

// Configure Authentication
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

// DbContext Configuration
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repository and Services Registration
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var jwtSecret = config["JwtSecret"];
    return new TokenService(jwtSecret);
});
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddSingleton<IKeyVaultService, AzureKeyVaultService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();
builder.Services.AddSingleton<IRecipeParser, RecipeParser>();
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

    // Define the OAuth2 scheme that's in use (i.e. Implicit Flow)
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
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

//Ef Core migration
if (Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME") != null)
{
    using (var scope = builder.Services.BuildServiceProvider().CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        dbContext.Database.Migrate();
    }
}

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
        c.DefaultModelExpandDepth(2);
        //c.DefaultModelRendering(ModelRendering.Model);  // Commented out due to a missing namespace
        c.DefaultModelsExpandDepth(-1);
        c.DisplayRequestDuration();
        c.EnableDeepLinking();
        c.EnableFilter();
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
