namespace NewCityInfo.API.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;

        
        public int NumberOfPointOfInterest
        {
            get
            {
                return PointOfInterests.Count;
            }
        }
        public ICollection<PointOfInterestDto> PointOfInterests { get; set; } =
            new List<PointOfInterestDto>();


    }
}
