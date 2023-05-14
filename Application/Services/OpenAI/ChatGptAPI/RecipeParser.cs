using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class RecipeParser : IRecipeParser
    {
        private readonly FoodInformationParser _foodInformationParser = new FoodInformationParser();
        private readonly ILogger _logger;

        public RecipeParser(ILogger<RecipeParser> logger)
        {
            _logger = logger;
        }

        public GeneratedRecipeDTO Parse(string assistantMessage)
        {
            var lines = assistantMessage.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var generatedRecipe = new GeneratedRecipeDTO();

            try
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("Food information:"))
                    {
                        generatedRecipe.FoodInformation = _foodInformationParser.Parse(lines, ref i);
                    }
                }
            }
            catch (Exception ex)
            {
                // Log the error and rethrow
                _logger.LogError(ex, "Error parsing recipe");
                throw;
            }

            return generatedRecipe;
        }
    }
    public class FoodInformationParser
    {
        public FoodInformationDTO Parse(string[] lines, ref int i)
        {
            var foodInformation = new FoodInformationDTO();

            // Skip the "Food information:" line
            i++;

            while (i < lines.Length && !lines[i].StartsWith("List of ingredients:") && !lines[i].StartsWith("Cooking steps:"))
            {
                var line = lines[i];

                if (line.StartsWith("Name:"))
                {
                    foodInformation.Name = line.Substring("Name:".Length).Trim();
                }
                else if (line.StartsWith("Description:"))
                {
                    foodInformation.Description = line.Substring("Description:".Length).Trim();
                }
                else if (line.StartsWith("Preparation time:"))
                {
                    var preparationTimeStr = line.Substring("Preparation time:".Length).Trim().Replace(" minutes", "");
                    foodInformation.PreparationTime = TimeSpan.FromMinutes(int.Parse(preparationTimeStr));
                }
                else if (line.StartsWith("Cooking time:"))
                {
                    foodInformation.CookingTime = int.Parse(line.Substring("Cooking time:".Length).Trim().Replace(" minutes", ""));
                }
                else if (line.StartsWith("Servings:"))
                {
                    foodInformation.Servings = int.Parse(line.Substring("Servings:".Length).Trim());
                }
                else if (line.StartsWith("Calories per serving:"))
                {
                    foodInformation.CaloriesPerServing = int.Parse(line.Substring("Calories per serving:".Length).Trim());
                }
                else if (line.StartsWith("Serving size:"))
                {
                    foodInformation.ServingSize = int.Parse(line.Substring("Serving size:".Length).Trim().Replace(" cups", ""));
                }
                else if (line.StartsWith("Dietary preferences:"))
                {
                    foodInformation.DietaryPreferences = line.Substring("Dietary preferences:".Length).Trim();
                }
                else if (line.StartsWith("Key ingredients:"))
                {
                    foodInformation.KeyIngredients = line.Substring("Key ingredients:".Length).Trim();
                }
                else if (line.StartsWith("Allergy restrictions:"))
                {
                    foodInformation.AllergyRestrictions = line.Substring("Allergy restrictions:".Length).Trim();
                }
                else if (line.StartsWith("Cuisine:"))
                {
                    foodInformation.Cuisine = line.Substring("Cuisine:".Length).Trim();
                }
                else if (line.StartsWith("Dish type:"))
                {
                    foodInformation.DishType = line.Substring("Dish type:".Length).Trim();
                }
                else if (line.StartsWith("Cooking method:"))
                {
                    foodInformation.CookingMethod = line.Substring("Cooking method:".Length).Trim();
                }
                i++;
            }

            return foodInformation;
        }
    }



}




