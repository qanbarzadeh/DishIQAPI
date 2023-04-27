using Application.DTO.RecipeDTOs;
using AutoMapper;
using Domain.Entities.RecipeEntities;

namespace Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<RecipeRequest, RecipeRequestDTO>();
            CreateMap<RecipeRequestDTO, RecipeRequest>();
        }
    }
}
