using Domain.Enums.RecipeEnums;

namespace Domain.Entities.RecipeEntities
{
    public class Flavor
    {
        public int Id { get; set; }
        //public string Name { get; set; } = "None";
        public FlavorEnum FlavorType { get; set; } = FlavorEnum.None;
    }
}
