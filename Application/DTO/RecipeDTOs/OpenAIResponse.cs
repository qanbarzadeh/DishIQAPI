using Application.DTO.OpenAiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.RecipeDTOs
{
    public class OpenAIResponse
    {
        public int Id { get; set; }
        public string Object { get; set; }
        public DateTime Created { get; set; }
        public string Model { get; set; }
        public Dictionary<string, object> Usage { get; set; }
        public List<Choice> Choices { get; set; }
    }

}
