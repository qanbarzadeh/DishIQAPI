using Application.DTO;
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
            var recipeRequestDTO = new RecipeRequestDTO
            {
                MealType = "SomeMealType",
                DietPreference = "SomeDietPreference",
                Region = "SomeRegion",
                CookingTechnique = "SomeCookingTechnique",
                NumberOfPax = 2,
                Country = "SomeCountry",
                MealTime = "SomeMealTime",
                BloodType = "SomeBloodType"
            };
            var expectedGeneratedRecipe = new GeneratedRecipe
            {
                GeneratedRecipeID = 1,
                
                FoodInformation = new FoodInformation { Id = 1, Name = "Test Food",
                Description = "Description test"},
                // Add hardcoded Ingredients and CookingSteps here
            };
            mockedChatGptService.Setup(api => api.GeneratedRecipeApiAsync(recipeRequestDTO)).ReturnsAsync(expectedGeneratedRecipe);
            var recipeService = new RecipeService(mockedChatGptService.Object);

            //Act 
            var actualGeneatedRecipe = await recipeService.GetGeneratedRecipeAsync(recipeRequestDTO);
            

            //Assert 
            Assert.NotNull(actualGeneatedRecipe);
            Assert.Equal(1, actualGeneatedRecipe.GeneratedRecipeID);
            Assert.Equal("Test Food", actualGeneatedRecipe.FoodInformation.Name);
            Assert.Equal("Description test", actualGeneatedRecipe.FoodInformation.Description);
        }               
    }
}
