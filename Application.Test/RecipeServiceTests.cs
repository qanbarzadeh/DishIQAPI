using Application.DTO;
using Application.DTO.RecipeDTOs;
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
            var expectedGeneratedRecipeDTO = new GeneratedRecipeDTO
            {                
                FoodInformation = new FoodInformationDTO { Name = "Test Food", Description = "Description test" },
                Ingredients = new List<IngredientDTO>
                {
                    new IngredientDTO { Id = 1, Name = "Ingredient 1", Quantity = "2" },
                    new IngredientDTO { Id = 2, Name = "Ingredient 2", Quantity = "1" }
                },
                CookingSteps = new List<CookingStepDTO>
                {
                    new CookingStepDTO { Id = 1, Description = "Cooking step 1" },
                    new CookingStepDTO { Id = 2, Description = "Cooking step 2" }
                }
            };


            mockedChatGptService.Setup(api => api.GeneratedRecipeApiAsync(recipeRequestDTO)).ReturnsAsync(expectedGeneratedRecipeDTO);
            var recipeService = new RecipeService(mockedChatGptService.Object);

            //Act 
            var actualGeneatedRecipe = await recipeService.GetGeneratedRecipeAsync(recipeRequestDTO);
            

            //Assert 
            Assert.NotNull(actualGeneatedRecipe);            
            Assert.Equal("Test Food", actualGeneatedRecipe.FoodInformation.Name);
            Assert.Equal("Description test", actualGeneatedRecipe.FoodInformation.Description);
        }               
    }
}
