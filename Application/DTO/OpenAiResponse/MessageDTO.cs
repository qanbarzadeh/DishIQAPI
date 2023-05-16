using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.OpenAiResponse
{
    public class MessageDTO
    {
        public string Role { get; set; }
        public string Content { get; set; }
    }
}
