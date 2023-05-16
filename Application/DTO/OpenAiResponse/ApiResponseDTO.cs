using Application.DTO.RecipeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.OpenAiResponse
{
    public class ApiResponseDTO
    {
        public string Id { get; set; }
        public string Object { get; set; }
        public int Created { get; set; }
        public string Model { get; set; }
        public UsageDTO Usage { get; set; }
        public List<ChoiceDTO> Choices { get; set; }
    }
}
