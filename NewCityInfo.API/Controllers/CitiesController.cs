using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewCityInfo.API.Entities;
using NewCityInfo.API.Models;
using NewCityInfo.API.Services;
using System.Text.Json;

namespace NewCityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/cities")]

    public class CitiesController : ControllerBase
    {

        private readonly ICityInfoRepository _repository;
        private readonly IMapper _mapper;
        const int defaultpageSize = 20;

        public CitiesController(ICityInfoRepository repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if(pageSize >= defaultpageSize)
            {
                pageSize = defaultpageSize;
            }
            var (cities, paginationMetadata) = await _repository.GetCitiesAsync(name, searchQuery, pageNumber, pageSize);
            if(!cities.Any() || cities == null)
            {
                return NotFound();
            }
            var map = _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities);

            Response.Headers.Add("x-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(map);

        }
        [HttpGet("{cityId}")]
        public async Task<IActionResult> GetCity(int cityId, bool includePopintOfInterest = false)

        {
            var city = await _repository.GetCityAsyn(cityId, includePopintOfInterest);

            if (city == null)
            {
                return NotFound();
            }
            if (!includePopintOfInterest)
            {

                return Ok(_mapper.Map<CityWithoutPointOfInterestDto>(city));

            }
            CityDto cityDto = new CityDto
            {
                Description = city.Description,
                Id = city.Id,
                Name = city.Name,
                PointOfInterests = new List<PointOfInterestDto>() 
            };
            foreach (var ele in city.pointOfInterestsInCity)
            {
                var pointofinterest = new PointOfInterestDto
                {
                    Id = ele.Id,
                    Name = ele.Name,
                    Description = ele.Description
                };

                cityDto.PointOfInterests.Add(pointofinterest);
            }


            return Ok(cityDto);





        }

    }
}
