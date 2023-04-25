using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Application.Mapping;
using Application.Services.Recipe;
using Microsoft.Extensions.Configuration;
using Application.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddHttpClient<IChatGptService, ChatGptService>();

// Register RecipeService
builder.Services.AddScoped<IRecipeService, RecipeService>();
// Configure RapidApiOptions using environment variables
var rapidApiKey = Environment.GetEnvironmentVariable("RAPIDAPI_KEY");
var rapidApiHost = Environment.GetEnvironmentVariable("RAPIDAPI_HOST");
var rapidApiEndpoint = Environment.GetEnvironmentVariable("RAPIDAPI_ENDPOINT");
builder.Services.Configure<RapidApiOptions>(options =>
{
    options.RAPIDAPI_KEY = rapidApiKey;
    options.RAPIDAPI_HOST = rapidApiHost;
    options.RAPIDAPI_ENDPOINT = rapidApiEndpoint;
});



// Register AutoMapper
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); //Register IMapper when need to store entityinto databse 

//builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // check if it is needed to inject Iconfiguration here. 
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
