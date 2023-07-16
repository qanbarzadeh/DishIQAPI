using Application.Interfaces.NutritionsAnalysis;
using Domain.Entities.RecipeEntities;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly AppDbContext _context; 

        public RecipeRepository(AppDbContext context)
        {
            _context = context;
        }        
        public async Task AddRecipeAsync(Recipe recipe)
        {
            
            if (recipe == null) throw new ArgumentNullException(nameof(recipe));  
            await  _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await _context.Recipes.ToListAsync();
            
        }
        public async Task<Recipe> GetRecipeByIdAsync(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                throw new KeyNotFoundException($"Recipe with id {id} was not found in the database.");
            }
            return recipe;
        }

    }
}
