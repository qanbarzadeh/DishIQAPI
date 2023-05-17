using Application.DTO.OpenAiResponse;
using Application.DTO.RecipeDTOs;
using Domain.Entities.RecipeEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.OpenAI.ChatGptAPI
{
    public interface IChatGptService
    {
        Task<ApiResponseDTO> GeneratedRecipeApiAsync(RecipeRequestDTO recipeRequestDTO);
    }
}

