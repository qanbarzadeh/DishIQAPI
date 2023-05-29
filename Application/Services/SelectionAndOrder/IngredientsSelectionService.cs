using Application.DTO.IngredientSelection;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.SelectionAndOrder
{
    public class IngredientsSelectionService : IIngredientsSelectionService
    {
        public Task<List<StoreDTO>> GetStoresForIngredientsAsync(List<string> ingredients)
        {
            var stores = new List<StoreDTO>
            {
                new StoreDTO { StoreName = "Store A", StoreAddress = "Address A", Distance = 5.1 ,AvailableIngredients = new List<string> { "Ingredient 1", "Ingredient 2" } },
                new StoreDTO { StoreName = "Store B", StoreAddress = "Address B", Distance = 2.5, AvailableIngredients = new List<string> { "Ingredient 2", "Ingredient 3" } },
                // Add more stores as needed
            };
            return Task.FromResult(stores);
        }
    }
}
