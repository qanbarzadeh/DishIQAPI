using Application.DTO.IngredientSelection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIngredientsSelectionService
    {
        Task<List<StoreDTO>> GetStoresForIngredientsAsync(List<string> ingredients);
    }
}
