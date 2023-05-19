using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.IngredientSelection
{
    public class StoreDTO
    {
        public string? StoreName { get; set; }
        public string? StoreAddress { get; set; }
        public double Distance { get; set; }
        public List<string>? AvailableIngredients { get; set; }
    }

}
