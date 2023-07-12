namespace Application.DTO.OpenAiResponse
{
    public class ChoiceDTO
    {
        public MessageDTO Message { get; set; }
        public string FinishReason { get; set; }
        public int Index { get; set; }
    }
}
