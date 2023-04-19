using Application.Services.OpenAI.ChatGptAPI;
using Application.Services.Recipe;
using Domain.Entities.RecipeEntities;
using Domain.ValueObjects.Recipe;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test
{
    public class RecipeServiceTests
    {

      private readonly IRecipeService _recipeService;

        [Fact]
        public async Task GetGeneratedRecipeAsync_ReturnGeneratedRecipe()
        {
            //Arrange
            var mockedChatGptService = new Mock<IChatGptService>();
            var recipeRequest = new RecipeRequest
            {
                MealTypeId = 1,
                DietPreferenceId = 1,
                RegionId = 1,
                CookingTechniqueId = 1,
                NumberOfPax = 2,
                CountryId = 1,
                MealTimeId = 1,
                BloodTypeId = 1
            };
            var expectedGeneratedRecipe = new GeneratedRecipe
            {
                GeneratedRecipeID = 1,
                
                FoodInformation = new FoodInformation { Id = 1, Name = "Test Food",
                Description = "Description test"},
                // Add hardcoded Ingredients and CookingSteps here
            };
            mockedChatGptService.Setup(api => api.GeneratedRecipeApiAsync(recipeRequest)).ReturnsAsync(expectedGeneratedRecipe);
            var recipeService = new RecipeService(mockedChatGptService.Object);

            //Act 
            var actualGeneatedRecipe = await recipeService.GetGeneratedRecipeAsync(recipeRequest);
            

            //Assert 
            Assert.NotNull(actualGeneatedRecipe);
            Assert.Equal(1, actualGeneatedRecipe.GeneratedRecipeID);
            Assert.Equal("Test Food", actualGeneatedRecipe.FoodInformation.Name);
            Assert.Equal("Description test", actualGeneatedRecipe.FoodInformation.Description);
        }               
    }
}
