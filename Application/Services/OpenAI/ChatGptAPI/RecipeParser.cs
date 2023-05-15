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
            // Assuming the content is a JSON string and the structure matches the DTOs.
            // If the structure is different, you will need to write more complex parsing logic.

            JObject jObject = JObject.Parse(content);

            // Parse the Food Information
            var foodInformation = jObject["foodInformation"];
            FoodInformationDTO foodInformationDTO = new FoodInformationDTO
            {
                Name = (string)foodInformation["name"],
                Description = (string)foodInformation["description"],
                PreparationTime = (string)foodInformation["preparationTime"],
                CookingTime = (string)foodInformation["cookingTime"],
                Servings = (string)foodInformation["servings"],
                CaloriesPerServing = (string)foodInformation["caloriesPerServing"],
                ServingSize = (string)foodInformation["servingSize"],
                DietaryPreferences = (string)foodInformation["dietaryPreferences"],
                KeyIngredients = (string)foodInformation["keyIngredients"],
                AllergyRestrictions = (string)foodInformation["allergyRestrictions"],
                Cuisine = (string)foodInformation["cuisine"],
                DishType = (string)foodInformation["dishType"],
                CookingMethod = (string)foodInformation["cookingMethod"],
            };

            // TODO: Parse the Ingredients and CookingSteps

            GeneratedRecipeDTO generatedRecipeDTO = new GeneratedRecipeDTO
            {
                FoodInformation = foodInformationDTO,
                // Assign parsed Ingredients and CookingSteps here
            };

            return generatedRecipeDTO;
        }
    }

}

