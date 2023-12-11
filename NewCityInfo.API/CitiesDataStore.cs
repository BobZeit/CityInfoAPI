using Microsoft.OpenApi.Services;
using NewCityInfo.API.Models;

namespace NewCityInfo.API
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; } 


        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The One with that big prak.",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto
                        {
                            Id= 1,
                            Name = "central park",
                            Description = "The most visited urban park in US"
                        },
                        new PointOfInterestDto
                        {
                            Id= 2,
                            Name = "Empire State Bulding",
                            Description = "A 102-story skyscraper"
                        }
                    }

    },
                new CityDto()
                {
                    Id =2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never finished.",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                         new PointOfInterestDto
                        {
                            Id= 3,
                            Name = "point of interest 3",
                            Description = "Description 3"
                        },
                        new PointOfInterestDto
                        {
                            Id= 4,
                            Name = "point of interest 4",
                            Description = "Description 4"
                        }
                    }
                },
                new CityDto()
                {
                    Id =3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                     PointOfInterests = new List<PointOfInterestDto>
                    {
                         new PointOfInterestDto
                        {
                            Id= 5,
                            Name = "point of interest 5",
                            Description = "Description 5"
                        },
                        new PointOfInterestDto
                        {
                            Id= 6,
                            Name = "point of interest 6",
                            Description = "Description 6"
                        }
                    }
                }

            };
        }
    }
}
