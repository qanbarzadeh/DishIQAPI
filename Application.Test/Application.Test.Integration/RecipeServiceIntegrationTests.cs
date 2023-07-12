namespace Application.Test.Application.Test.Integration
{
    //public class RecipeServiceIntegrationTests
    //{

    //    private readonly ILogger _logger;
    //    private readonly IOptions<RapidApiOptions> _rapidAiOptions;
    //    private readonly IRecipeParser _recipeParser; 

    //    public RecipeServiceIntegrationTests(ILogger<RecipeServiceIntegrationTests> logger, IOptions<RapidApiOptions> rapidAiOptions, IRecipeParser recipeParser)
    //    {
    //        _logger = logger;
    //        _rapidAiOptions = rapidAiOptions;
    //        _recipeParser = recipeParser;
    //    }

    //    [Fact]

    //    public async Task GetGeneratedRecipeAsync_IntegrationTest()
    //    {
    //        // Configure in-memory DbContext
    //        var options = new DbContextOptionsBuilder<AppDbContext>()
    //            .UseInMemoryDatabase(databaseName: "ChefblockTestDb")
    //            .Options;

    //        using var context = new AppDbContext(options);

    //        // Create HttpClient
    //        var httpClient = new HttpClient();

    //        // Configure appsettings.json
    //        var configuration = new ConfigurationBuilder()                    
    //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    //        .Build();

    //        // Create services
    //        var chatGptService = new ChatGptService(httpClient, configuration, _rapidAiOptions, (ILogger<ChatGptService>)_logger, _recipeParser); 



    //        // Perform the integration test
    //        var recipeRequestDTO = new RecipeRequestDTO
    //        {
    //            MealType = "SomeMealType",
    //            DietPreference = "SomeDietPreference",
    //            Region = "SomeRegion",
    //            CookingTechnique = "SomeCookingTechnique",
    //            NumberOfPax = 2,
    //            Country = "SomeCountry",
    //            MealTime = "SomeMealTime",
    //            BloodType = "SomeBloodType"
    //        };

    //        var generatedRecipe = await recipeService.GetGeneratedRecipeAsync(recipeRequestDTO);

    //        // Assert the expected results
    //        Assert.NotNull(generatedRecipe);
    //        // Add more assertions as necessary

    //    }

    //}
}

