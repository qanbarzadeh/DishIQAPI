
using Application.Services.OpenAI.ChatGptAPI;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Application.Mapping;
using Application.Services.Recipe;
using Application.Interfaces;
using OpenAIAPI;
using Application.Services.SelectionAndOrder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container..
builder.Services.AddControllers();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddHttpClient<IChatGptService, ChatGptService>();
builder.Services.AddScoped<IRecipeInformationService, RecipeInformationService>();
builder.Services.AddScoped<IIngredientsSelectionService, IngredientsSelectionService>(); 

// Register RecipeService 

builder.Services.AddSingleton<IRecipeParser, RecipeParser>();


// Register AutoMapper
//builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly); //Register IMapper when need to store entityinto databse 
//builder.Services.AddSingleton<IConfiguration>(builder.Configuration); // check if it is needed to inject Iconfiguration here. 
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.SetMinimumLevel(LogLevel.Information);

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

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
});
app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyHeader().AllowAnyMethod();
});

//app.UseHttpsRedirection();

//app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();

app.Run();
