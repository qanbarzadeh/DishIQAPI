namespace Application.Test.ServiceTest.ChatGptTests
{
    //public class ChatGptServiceTests
    //{
    //    [Fact]
    //    public async Task GeneratedRecipeApiAsync_ReturnsGeneratedRecipe()
    //    {
    //        // Arrange
    //        var configurationMock = new Mock<IConfiguration>();
    //        configurationMock.Setup(x => x["OpenAI:ApiKey"]).Returns("test_api_key");

    //        var httpClientHandlerMock = new Mock<HttpMessageHandler>();

    //        httpClientHandlerMock.Protected().Setup<Task<HttpResponseMessage>>("SendAsync",
    //                ItExpr.IsAny<HttpRequestMessage>(),
    //                ItExpr.IsAny<CancellationToken>())
    //            .ReturnsAsync(new HttpResponseMessage
    //            {
    //                StatusCode = HttpStatusCode.OK,
    //                Content = new StringContent(JsonConvert.SerializeObject(new GeneratedRecipe()))
    //            });

    //        var httpClient = new HttpClient(httpClientHandlerMock.Object);
    //        var rapidApiOptionsMock = new Mock<IOptions<RapidApiOptions>>();
    //        rapidApiOptionsMock.Setup(x => x.Value).Returns(new RapidApiOptions { RAPIDAPI_KEY = "test_api_key" });
    //        var loggerMock = new Mock<ILogger<ChatGptService>>();



    //        var chatGptService = new ChatGptService(httpClient, configurationMock.Object,rapidApiOptionsMock.Object, loggerMock.Object);

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

    //        // Act
    //        var generatedRecipe = await chatGptService.GeneratedRecipeApiAsync(recipeRequestDTO);

    //        // Assert
    //        Assert.NotNull(generatedRecipe);
    //        // Add more assertions to check the properties of the generated recipe as necessary
    //    }
    //}
}
