using System.ComponentModel.DataAnnotations;

namespace NewCityInfo.API.Models
{
    public class CityWithoutPointOfInterestDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
