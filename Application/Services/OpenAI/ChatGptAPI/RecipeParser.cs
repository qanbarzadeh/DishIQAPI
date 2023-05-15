using Application.DTO.RecipeDTOs;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public class RecipeParser : IRecipeParser
    {
        public GeneratedRecipeDTO Parse(string content)
        {
            JObject jObject = JObject.Parse(content);
            var message = jObject["choices"][0]["message"]["content"].ToString();

            // Parse the Food Information
            var foodInformationString = message.Substring(message.IndexOf("Food Information:"), message.IndexOf("List of Ingredients:") - message.IndexOf("Food Information:")).Trim();
            var foodLines = foodInformationString.Split("\n");
            var name = foodLines[1].Split(":")[1].Trim();
            var description = foodLines[2].Split(":")[1].Trim();
            var preparationTime = foodLines[3].Split(":")[1].Trim();
            var cookingTime = foodLines[4].Split(":")[1].Trim();
            var servings = foodLines[5].Split(":")[1].Trim();
            var caloriesPerServing = foodLines[6].Split(":")[1].Trim();
            var servingSize = foodLines[7].Split(":")[1].Trim();
            var dietaryPreferences = foodLines[8].Split(":")[1].Trim();
            var keyIngredients = foodLines[9].Split(":")[1].Trim();
            var allergyRestrictions = foodLines[10].Split(":")[1].Trim();
            var cuisine = foodLines[11].Split(":")[1].Trim();
            var dishType = foodLines[12].Split(":")[1].Trim();
            var cookingMethod = foodLines[13].Split(":")[1].Trim();

            FoodInformationDTO foodInformationDTO = new FoodInformationDTO
            {
                Name = name,
                Description = description,
                PreparationTime = preparationTime,
                CookingTime = cookingTime,
                Servings = servings,
                CaloriesPerServing = caloriesPerServing,
                ServingSize = servingSize,
                DietaryPreferences = dietaryPreferences,
                KeyIngredients = keyIngredients,
                AllergyRestrictions = allergyRestrictions,
                Cuisine = cuisine,
                DishType = dishType,
                CookingMethod = cookingMethod
            };

            // Parse the Ingredients
            var ingredientsString = message.Substring(message.IndexOf("List of Ingredients:"), message.IndexOf("Cooking Steps:") - message.IndexOf("List of Ingredients:")).Trim();
            var ingredientsLines = ingredientsString.Split("\n").Skip(1); // Skip the "List of Ingredients:" line
            List<IngredientDTO> ingredients = ingredientsLines.Select(line =>
            {
                var parts = line.Split(",");
                return new IngredientDTO
                {
                    Name = parts[0].Trim(),
                    Quantity = parts[1].Trim(),
                    Unit = parts.Length > 2 ? parts[2].Trim() : null
                };
            }).ToList();

            // Parse the Cooking Steps
            var cookingStepsString = message.Substring(message.IndexOf("Cooking Steps:")).Trim();
            var cookingStepsLines = cookingStepsString.Split("\n").Skip(1); // Skip the "Cooking Steps:" line
            List<CookingStepDTO> cookingSteps = cookingStepsLines.Select(line =>
            {
                var parts = line.Split(".");
                return new CookingStepDTO
                {
                    Order = parts[0].Trim(),
                    Description = parts[1].Trim()
                };
            }).ToList();

            // Populate the GeneratedRecipeDTO
            GeneratedRecipeDTO generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformationDTO,
                Ingredients = ingredients,
                CookingSteps = cookingSteps
            };

            return generatedRecipeDTO;
        }
    }
}

