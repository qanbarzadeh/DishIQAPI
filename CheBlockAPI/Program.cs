using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Application.Services.OpenAI.ChatGptAPI;
using Microsoft.Extensions.DependencyInjection;
using Application.Mapping;
using Application.Services.Recipe;

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

// Register ChatGptService
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();

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
