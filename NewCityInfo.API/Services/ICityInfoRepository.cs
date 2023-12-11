using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;
using NewCityInfo.API.Entities;

namespace NewCityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        
        Task<City?> GetCityAsyn(int cityId, bool includepointOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointOfinterestsAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfinterestId);
        Task<bool> CityExistAsync(int cityId);
        Task AddPointOfInterest(int cityId, PointOfInterest pointOfInterest);

        Task<bool> SaveChagesAsync();

        Task<PointOfInterest?> RemovePointOfInterest(int cityId, int pointOfInterestId);
        Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);

    }
}
