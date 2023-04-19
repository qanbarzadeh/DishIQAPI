using Domain.Entities.RecipeEntities;
using Domain.ValueObjects.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class ChatGptService : IChatGptService
    {
        public async Task<GeneratedRecipe> GeneratedRecipeApiAsync(RecipeRequest request)
        {
            // You should add any necessary dependencies and configuration for interacting with the ChatGPT API here.

            var  GeneratedRecipe = new GeneratedRecipe
            {
                GeneratedRecipeID = 1,              
                FoodInformation = new FoodInformation { Id = 1,
                    Name = "Example Food",
                    Description = "This is a good food ", 
                    CookingTime = 34, 
                    Servings = 1, 
                   },

                Ingredients = new List<Ingredient>
                {
                    new Ingredient { Id = 1, Name = "Ingredient 1", Unit = "g", Quantity = 100 },
                    new Ingredient { Id = 2, Name = "Ingredient 2", Unit = "ml", Quantity = 50 }
                },
                CookingSteps = new List<CookingStep>
                {
                    new CookingStep { Id = 1, Description = "Step 1: Prepare the ingredients", Order = 1 },
                    new CookingStep { Id = 2, Description = "Step 2: Cook the dish", Order = 2 }
                }
            };

            return await Task.FromResult(GeneratedRecipe);
        }
    }
}
