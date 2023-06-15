namespace Application.DTO.Azure.maps
{
    public class SearchRequestDTO
    {
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int Radius { get; set; }
    }
}
