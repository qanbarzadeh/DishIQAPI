using Application.Interfaces.NutritionsAnalysis;
using Domain.Entities.RecipeEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RecipeIngredientRepository : IRecipeIngredientRepository
    {
        private readonly AppDbContext _context;

        public RecipeIngredientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<RecipeIngredient> GetRecipeIngredientByIdAsync(int id)
        {
            var recipeIngredient = await _context.RecipeIngredients.FindAsync(id);
            if (recipeIngredient == null)
            {
                throw new KeyNotFoundException($"Recipe Ingredient with id {id} not found");
            }

            return recipeIngredient;
        }

        public async Task<IEnumerable<RecipeIngredient>> GetAllRecipeIngredientsAsync()
        {
            var recipeIngredients = await _context.RecipeIngredients.ToListAsync();
            if (recipeIngredients.Count == 0)
            {
                throw new Exception("No recipe ingredients found");
            }

            return recipeIngredients;
        }

        public async Task AddRecipeIngredientAsync(RecipeIngredient recipeIngredient)
        {
            if (recipeIngredient == null)
            {
                throw new ArgumentNullException(nameof(recipeIngredient), "Provided recipe ingredient is null");
            }

            try
            {
                await _context.RecipeIngredients.AddAsync(recipeIngredient);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception("Failed to add recipe ingredient", e);
            }
        }


        //***we should create a loggin mechanism and log all exceptions***// 

        // Implement additional methods as needed
    }
}
