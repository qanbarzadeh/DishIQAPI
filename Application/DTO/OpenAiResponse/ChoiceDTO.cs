using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.OpenAiResponse
{
    public class ChoiceDTO
    {
        public MessageDTO Message { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }
}
