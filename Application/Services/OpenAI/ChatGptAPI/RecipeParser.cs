using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class RecipeParser : IRecipeParser
    {
        private readonly ILogger<RecipeParser> _logger;

        public RecipeParser(ILogger<RecipeParser> logger)
        {
            _logger = logger;
        }


        public GeneratedRecipeDTO Parse(string assistantMessage)
        {
            // Split the assistant message into lines
            var lines = assistantMessage.Split('\n');

            // Create the RecipeResponseDTO
            var generatedRecipeDTO = new GeneratedRecipeDTO();

            // Temporary holders for ingredients and cooking steps
            var ingredients = new List<IngredientDTO>();
            var cookingSteps = new List<CookingStepDTO>();
            // Create a new FoodInformationDTO
            var foodInformation = new FoodInformationDTO();

            // Flags to track when we're in the ingredients or cooking steps sections
            bool inIngredients = false;
            bool inCookingSteps = false;

            // Loop through each line
            for (int i = 0; i < lines.Length; i++)
            {
                // Get current line
                var line = lines[i].Trim();

                // Check if we're entering or leaving the ingredients or cooking steps sections
                if (line.StartsWith("List of Ingredients:"))
                {
                    inIngredients = true;
                    continue;
                }
                else if (line.StartsWith("Cooking Steps:"))
                {
                    inIngredients = false;
                    inCookingSteps = true;
                    continue;
                }
                else if (line.StartsWith("Food Information:"))
                {
                    inCookingSteps = false;
                    continue;
                }

                try
                {
                    // Parse the food information
                    if (!inIngredients && !inCookingSteps)
                    {
                        if (line.StartsWith("Name:"))
                            foodInformation.Name = line.Substring("Name:".Length).Trim();
                        else if (line.StartsWith("Description:"))
                            foodInformation.Description = line.Substring("Description:".Length).Trim();
                        // ... repeat this for all properties of FoodInformationDTO ...
                    }

                    // Parse the list of ingredients
                    else if (inIngredients)
                    {
                        var parts = line.Split('-');
                        if (parts.Length > 1)
                        {
                            var ingredient = new IngredientDTO
                            {
                                Name = parts[1].Trim(),  // remove the '-' at the start
                                                         // Quantity and Unit are omitted here as they're not provided in the response
                            };
                            ingredients.Add(ingredient);
                        }
                    }

                    // Parse the cooking steps
                    else if (inCookingSteps)
                    {
                        var stepLine = line;
                        var cookingStep = new CookingStepDTO
                        {
                            Order = int.Parse(stepLine.Split('.')[0]),  // parse the step number
                            Description = stepLine.Substring(stepLine.IndexOf('.') + 2)  // get the step description
                        };
                        cookingSteps.Add(cookingStep);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Failed to parse line {i}: {line}");
                    throw;
                }              
            }
            // Assign the parsed ingredients and cooking steps to the RecipeResponseDTO
            generatedRecipeDTO.Ingredients = ingredients;
            generatedRecipeDTO.CookingSteps = cookingSteps;
            generatedRecipeDTO.FoodInformation = foodInformation;

            return generatedRecipeDTO;
        }
    }
}
