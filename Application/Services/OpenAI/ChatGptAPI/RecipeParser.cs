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
        private readonly ILogger<RecipeParser> _logger;

        public RecipeParser(ILogger<RecipeParser> logger)
        {
            _logger = logger;
        }

        public GeneratedRecipeDTO Parse(string assistantMessage)
        {
            var lines = assistantMessage.Split('\n');

            var foodInfoStart = Array.IndexOf(lines, "Food Information:") + 1;
            var ingredientsStart = Array.IndexOf(lines, "List of Ingredients:") + 1;
            var cookingStepsStart = Array.IndexOf(lines, "Cooking Steps:") + 1;

            var foodInfoLines = lines.Skip(foodInfoStart).Take(ingredientsStart - foodInfoStart - 2).ToList();
            var ingredientLines = lines.Skip(ingredientsStart).Take(cookingStepsStart - ingredientsStart - 2).ToList();
            var cookingStepLines = lines.Skip(cookingStepsStart).ToList();

            var foodInformation = ParseFoodInformation(foodInfoLines);
            var ingredients = ParseIngredients(ingredientLines);
            var cookingSteps = ParseCookingSteps(cookingStepLines);

            return new GeneratedRecipeDTO
            {
                FoodInformation = foodInformation,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };
        }

        private FoodInformationDTO ParseFoodInformation(List<string> lines)
        {
            var foodInformation = new FoodInformationDTO();

            foreach (var line in lines)
            {
                if (line.StartsWith("Name:"))
                    foodInformation.Name = line.Substring("Name:".Length).Trim();
                else if (line.StartsWith("Description:"))
                    foodInformation.Description = line.Substring("Description:".Length).Trim();
                else if (line.StartsWith("Preparation Time:"))
                    foodInformation.PreparationTime = ParseTime(line.Substring("Preparation Time:".Length).Trim());
                else if (line.StartsWith("Cooking Time:"))
                    foodInformation.CookingTime = ParseTime(line.Substring("Cooking Time:".Length).Trim());
                else if (line.StartsWith("Servings:"))
                    foodInformation.Servings = ParseServings(line.Substring("Servings:".Length).Trim());
                else if (line.StartsWith("Calories per Serving:"))
                    foodInformation.CaloriesPerServing = ParseCalories(line.Substring("Calories per Serving:".Length).Trim());
                else if (line.StartsWith("Serving Size:"))
                    foodInformation.ServingSize = ParseServingSize(line.Substring("Serving Size:".Length).Trim());
                else if (line.StartsWith("Dietary Preferences:"))
                    foodInformation.DietaryPreferences = line.Substring("Dietary Preferences:".Length).Trim();
                else if (line.StartsWith("Key Ingredients:"))
                    foodInformation.KeyIngredients = line.Substring("Key Ingredients:".Length).Trim();
                else if (line.StartsWith("Allergy Restrictions:"))
                    foodInformation.AllergyRestrictions = line.Substring("Allergy Restrictions:".Length).Trim();
                else if (line.StartsWith("Cuisine:"))
                    foodInformation.Cuisine = line.Substring("Cuisine:".Length).Trim();
                else if (line.StartsWith("Dish Type:"))
                    foodInformation.DishType = line.Substring("Dish Type:".Length).Trim();
                else if (line.StartsWith("Cooking Method:"))
                    foodInformation.CookingMethod = line.Substring("Cooking Method:".Length).Trim();
            }

            return foodInformation;
        }

        private List<IngredientDTO> ParseIngredients(List<string> lines)
        {
            var ingredients = new List<IngredientDTO>();

            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length > 1)
                {
                    var ingredientName = parts[1].Trim();
                    var ingredient = ParseIngredient(ingredientName);
                    ingredients.Add(ingredient);
                }
            }

            return ingredients;
        }
        private IngredientDTO ParseIngredient(string ingredientName)
        {
            var ingredient = new IngredientDTO();

            var parts = ingredientName.Split(':');
            if (parts.Length > 1)
            {
                ingredient.Name = parts[0].Trim();
                ingredient.Quantity = 0; // Initialize to zero or set the actual quantity if available
                ingredient.Unit = null; // Set to null or parse the unit if available
            }
            else
            {
                ingredient.Name = ingredientName.Trim();
                ingredient.Quantity = 0; // Initialize to zero or set the actual quantity if available
                ingredient.Unit = null; // Set to null or parse the unit if available
            }

            return ingredient;
        }



        private List<CookingStepDTO> ParseCookingSteps(List<string> lines)
        {
            var cookingSteps = new List<CookingStepDTO>();

            foreach (var line in lines)
            {
                var stepLine = line.Trim();
                var stepNumber = ParseStepNumber(stepLine);
                var stepDescription = ParseStepDescription(stepLine);

                var cookingStep = new CookingStepDTO
                {
                    Order = stepNumber,
                    Description = stepDescription
                };

                cookingSteps.Add(cookingStep);
            }

            return cookingSteps;
        }

        private int ParseStepNumber(string stepLine)
        {
            // Use regular expressions to extract the numeric part of the step number
            Match match = Regex.Match(stepLine, @"\d+");
            if (match.Success)
            {
                string numberString = match.Value;
                if (int.TryParse(numberString, out int stepNumber))
                {
                    return stepNumber;
                }
            }

            // Throw an exception if the step number cannot be parsed
            throw new FormatException("Invalid step number format: " + stepLine);
        }


        private string ParseStepDescription(string stepLine)
        {
            // Extract the step description from the stepLine
            var stepDescription = stepLine.Substring(stepLine.IndexOf('.') + 2).Trim();
            return stepDescription;
        }

        private int ParseTime(string timeString)
        {
            // You can implement your custom logic to parse the time string into an integer representation
            // For example, you can extract the numbers and convert them to minutes
            // You can also handle different time units (e.g., minutes, hours) based on your specific requirements
            // For demonstration purposes, let's assume the time string contains only an integer representing minutes
            if (int.TryParse(timeString, out int time))
            {
                return time;
            }
            else
            {
                // Unable to parse the time, return a default value (e.g., 0) or handle the error accordingly
                return 0;
            }
        }

        private int ParseCalories(string caloriesString)
        {
            // You can implement your custom logic to parse the calories string into an integer representation
            // For example, you can extract the numbers and convert them to an integer
            // For demonstration purposes, let's assume the calories string contains only an integer value
            if (int.TryParse(caloriesString, out int calories))
            {
                return calories;
            }
            else
            {
                // Unable to parse the calories, return a default value (e.g., 0) or handle the error accordingly
                return 0;
            }
        }

        private int ParseServingSize(string servingSizeString)
        {
            // You can implement your custom logic to parse the serving size string into an integer representation
            // For example, you can extract the numbers and convert them to an integer
            // For demonstration purposes, let's assume the serving size string contains only an integer value
            if (int.TryParse(servingSizeString, out int servingSize))
            {
                return servingSize;
            }
            else
            {
                // Unable to parse the serving size, return a default value (e.g., 0) or handle the error accordingly
                return 0;
            }
        }

        private int ParseServings(string servingsString)
        {
            // You can implement your custom logic to parse the servings string into an integer representation
            // For example, you can extract the numbers and convert them to an integer
            // For demonstration purposes, let's assume the servings string contains only an integer value
            if (int.TryParse(servingsString, out int servings))
            {
                return servings;
            }
            else
            {
                // Unable to parse the servings, return a default value (e.g., 0) or handle the error accordingly
                return 0;
            }
        }


    }
}



