using NewCityInfo.API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewCityInfo.API.Entities
{
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(200)]
        public string Description { get; set; } = string.Empty;
        public ICollection<PointOfInterest> pointOfInterestsInCity { get; set; } = new List<PointOfInterest>();
        public City(string name)
        {
            Name = name;
        }


    }
}
