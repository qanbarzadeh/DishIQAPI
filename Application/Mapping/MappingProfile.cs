using AutoMapper;
using Application.DTO.RecipeDTOs;
using Domain.Entities.RecipeEntities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GeneratedRecipeDTO, Recipe>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FoodInformation.Name))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.FoodInformation.Description))
                .ForMember(dest => dest.PreparationTime, opt => opt.MapFrom(src => src.FoodInformation.PreparationTime))
                .ForMember(dest => dest.CookingTime, opt => opt.MapFrom(src => src.FoodInformation.CookingTime))
                .ForMember(dest => dest.Servings, opt => opt.MapFrom(src => int.Parse(src.FoodInformation.Servings)))
                .ForMember(dest => dest.CaloriesPerServing, opt => opt.MapFrom(src => double.Parse(src.FoodInformation.CaloriesPerServing)))
                .ForMember(dest => dest.Cuisine, opt => opt.MapFrom(src => src.FoodInformation.Cuisine))
                .ForMember(dest => dest.DishType, opt => opt.MapFrom(src => src.FoodInformation.DishType))
                .ForMember(dest => dest.CookingMethod, opt => opt.MapFrom(src => src.FoodInformation.CookingMethod));
        }
    }
}