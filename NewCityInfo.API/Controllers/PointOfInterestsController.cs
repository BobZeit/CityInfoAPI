using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using NewCityInfo.API.Entities;
using NewCityInfo.API.Models;
using NewCityInfo.API.Services;

namespace NewCityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointofinterests")]
    [Authorize]
    [ApiController]
    public class PointOfInterestsController : ControllerBase

    {
        private readonly ILogger<PointOfInterestsController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _repository;
        private readonly IMapper _mapper;

        public PointOfInterestsController(ILogger<PointOfInterestsController> logger
            , IMailService mailService, ICityInfoRepository repository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointOfInterests(int cityId)
        {
            var cityExist = await _repository.CityExistAsync(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"city with Id {cityId} can't be found");
                return NotFound();
            }
            var pointOfInterests = await _repository.GetPointOfinterestsAsync(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterests));


        }
        [HttpGet("{pointOfInterestId}", Name = "GetPointOfInterets")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            var cityExist = await _repository.CityExistAsync(cityId);
            if (!cityExist)
            {
                _logger.LogInformation($"city with Id {cityId} can't be found");
                return NotFound();
            }
            var pointOfInterestToBeFound = await _repository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (pointOfInterestToBeFound == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterestToBeFound));


        }
        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatPointOfInterets(int cityId, PointOfInterestForPostDto postDto)
        {
            var cityToBeFound = await _repository.CityExistAsync(cityId);
            if (!cityToBeFound)
            {
                return NotFound();
            }
            var pointOfInterestToAdd = _mapper.Map<PointOfInterest>(postDto);
            await _repository.AddPointOfInterest(cityId, pointOfInterestToAdd);
            await _repository.SaveChagesAsync();
            var pointOfInterestToReturn = _mapper.Map<PointOfInterestDto>(pointOfInterestToAdd);
           
            return CreatedAtRoute("GetPointOfInterets",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = pointOfInterestToReturn.Id
                },
                pointOfInterestToReturn);


        }
        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpdatePointOfInterest(
            int cityId, int pointOfInterestId, PointOfInterestForUpdateDto updateDto)
        {

            if (!await _repository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = await _repository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            _mapper.Map(updateDto, pointOfInterest);
            await _repository.SaveChagesAsync();

            return NoContent();
        }
        [HttpPatch("{pointOfInterestId}")]
        public async Task<ActionResult> PatchPointOfInterest(
            int cityId, int pointOfInterestId,
            JsonPatchDocument<PointOfInterestForUpdateDto> patchDocument)
        {
            if(!await _repository.CityExistAsync(cityId))
            {
                return NotFound();
            }
            var pointOfInterest = await _repository.GetPointOfInterestAsync(cityId, pointOfInterestId);
            if(pointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestToBePatch = _mapper.Map<PointOfInterestForUpdateDto>(pointOfInterest);
            patchDocument.ApplyTo(pointOfInterestToBePatch, ModelState);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!TryValidateModel(pointOfInterestToBePatch))
            {
                return BadRequest(ModelState);
            }
            _mapper.Map(pointOfInterestToBePatch, pointOfInterest);
            await _repository.SaveChagesAsync();
            return NoContent();
        }
        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> DeletePointOfInterets(int cityId, int pointOfInterestId)
        {
            var pointOfInterest = await _repository.RemovePointOfInterest(cityId, pointOfInterestId);
            if(pointOfInterest == null)
            {
                return NotFound();
            }
            await _repository.SaveChagesAsync();
            _mailService.send("point of interest deleted", $"point of interest with id {pointOfInterest.Id} was deleted");
            return NoContent();
            
        }
    }
}
