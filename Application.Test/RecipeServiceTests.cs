using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Application.Services.OpenAI.ChatGptAPI;
using Application.Services.Recipe;
using Moq;
using Xunit;

public class RecipeServiceTests
{
    [Fact]
    public async Task GetGeneratedRecipeAsync_ReturnGeneratedRecipe()
    {
        // Arrange
        var mockedChatGptService = new Mock<IChatGptService>();
        var mockedRecipeParser = new Mock<IRecipeParser>();

        var recipeRequestDTO = new RecipeRequestDTO
        {
            MealType = "SomeMealType",
            DietaryPreference = "SomeDietPreference",
            Region = "SomeRegion",
            CookingTechnique = "SomeCookingTechnique",
            NumberOfPax = 2,
            Country = "SomeCountry",
            MealTime = "SomeMealTime",
            BloodType = "SomeBloodType"
        };

        var apiResponseDTO = new ApiResponseDTO
        {
            // fill this object with the expected values
        };

        var expectedGeneratedRecipeDTO = new GeneratedRecipeDTO
        {
            FoodInformation = new FoodInformationDTO { Name = "Test Food", Description = "Description test" },
            Ingredients = new List<IngredientDTO>
            {
                new IngredientDTO { IngredientInfo = "Ingredient 1" },
                new IngredientDTO { IngredientInfo = "Ingredient 2" }
            },
            CookingSteps = new List<CookingStepDTO>
            {
                new CookingStepDTO {Description = "Cooking step 1" },
                new CookingStepDTO { Description = "Cooking step 2" }
            }
        };

        mockedChatGptService.Setup(api => api.GeneratedRecipeApiAsync(recipeRequestDTO)).ReturnsAsync(apiResponseDTO);
        mockedRecipeParser.Setup(parser => parser.ParseFoodInformationFromContent(It.IsAny<string>())).Returns(expectedGeneratedRecipeDTO.FoodInformation);
        mockedRecipeParser.Setup(parser => parser.ParseIngredients(It.IsAny<string>())).Returns(expectedGeneratedRecipeDTO.Ingredients);
        mockedRecipeParser.Setup(parser => parser.ParseCookingSteps(It.IsAny<string>())).Returns(expectedGeneratedRecipeDTO.CookingSteps);

        var recipeService = new RecipeService(mockedChatGptService.Object, mockedRecipeParser.Object);

        // Act 
        var actualGeneratedRecipe = await recipeService.GetGeneratedRecipeAsync(recipeRequestDTO);

        // Assert 
        Assert.NotNull(actualGeneratedRecipe);
        Assert.Equal(expectedGeneratedRecipeDTO.FoodInformation.Name, actualGeneratedRecipe.FoodInformation.Name);
        Assert.Equal(expectedGeneratedRecipeDTO.FoodInformation.Description, actualGeneratedRecipe.FoodInformation.Description);
    }
}
