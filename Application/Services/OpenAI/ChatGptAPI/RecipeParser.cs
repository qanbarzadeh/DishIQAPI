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

            // Loop through each line
            for (int i = 0; i < lines.Length; i++)
            {
                // Get current line
                var line = lines[i];

                try
                {
                    // Parse the food information
                    if (line.StartsWith("- Name:"))
                        foodInformation.Name = line.Substring("- Name:".Length).Trim();
                    else if (line.StartsWith("- Description:"))
                        foodInformation.Description = line.Substring("- Description:".Length).Trim();
                    // ... repeat this for all properties of FoodInformationDTO ...

                    // Parse the list of ingredients
                    else if (line.StartsWith("List of Ingredients:"))
                    {
                        i++; // skip the "List of Ingredients:" line
                        while (!lines[i].StartsWith("Cooking Steps:"))
                        {
                            var ingredientLine = lines[i];
                            var parts = ingredientLine.Split(',');
                            var ingredient = new IngredientDTO
                            {
                                Name = parts[0].Substring(2).Trim(),  // remove the '-' at the start
                                Quantity = float.Parse(parts[1].Split(' ')[1]), // parse the quantity
                                Unit = parts[1].Split(' ')[2]  // get the unit
                            };
                            ingredients.Add(ingredient);
                            i++;
                        }
                    }

                    // Parse the cooking steps
                    else if (line.StartsWith("Cooking Steps:"))
                    {
                        i++; // skip the "Cooking Steps:" line
                        while (i < lines.Length)
                        {
                            var stepLine = lines[i];
                            var cookingStep = new CookingStepDTO
                            {
                                Order = int.Parse(stepLine.Split('.')[0]),  // parse the step number
                                Description = stepLine.Substring(stepLine.IndexOf('.') + 2)  // get the step description
                            };
                            cookingSteps.Add(cookingStep);
                            i++;
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error parsing line: {line}");
                    // Decide how to handle the error. You might want to continue parsing the rest of the lines, or stop and return an error.
                }
            }

            // Set the DTO properties
            generatedRecipeDTO.FoodInformation = foodInformation;
            generatedRecipeDTO.Ingredients = ingredients;
            generatedRecipeDTO.CookingSteps = cookingSteps;

            return generatedRecipeDTO;
        }
    }



}
