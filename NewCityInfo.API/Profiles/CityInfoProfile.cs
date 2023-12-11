using AutoMapper;
using NewCityInfo.API.Entities;
using NewCityInfo.API.Models;

namespace NewCityInfo.API.Profiles
{
    public class CityInfoProfile : Profile
    {
        public CityInfoProfile()
        {
            CreateMap<Entities.City, Models.CityWithoutPointOfInterestDto>();
            CreateMap<Entities.City, Models.CityDto>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
        }
    }
}
