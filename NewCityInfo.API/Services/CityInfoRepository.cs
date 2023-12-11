using Microsoft.EntityFrameworkCore;
using NewCityInfo.API.DbContexts;
using NewCityInfo.API.Entities;

namespace NewCityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CityExistAsync(int cityId)
        {
            var cityExist = await _context.Cities.AnyAsync(c => c.Id == cityId);
            return cityExist;
            
        }

      

        public async Task<City?> GetCityAsyn(int cityId, bool includepointOfInterest)
        {

            if (includepointOfInterest)
            {
                var city = await _context.Cities.Include(c => c.pointOfInterestsInCity)
                    .Where(c => c.Id == cityId).FirstOrDefaultAsync();
                return await _context.Cities.Include(c => c.pointOfInterestsInCity).OrderBy(c => c.Name)
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.OrderBy(c => c.Name)
                .Where(c => c.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestAsync(int cityId, int pointOfinterestId)
        {
            return await _context.PointOfInterests.OrderBy(p => p.Name).
                Where(p => p.Id == pointOfinterestId &&
            p.CityId == cityId).FirstOrDefaultAsync();

        }

        public async Task<IEnumerable<PointOfInterest>> GetPointOfinterestsAsync(int cityId)
        {
            return await _context.PointOfInterests.OrderBy(p => p.Name).Where(c => c.CityId == cityId)
                 .ToListAsync();
        }
        public async Task<bool> SaveChagesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
        public async Task AddPointOfInterest(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsyn(cityId, false);
            if (city != null)
            {
                city.pointOfInterestsInCity.Add(pointOfInterest);
            }
        }

        public async Task<PointOfInterest?> RemovePointOfInterest(int cityId, int pointOfInterestId)
        {
            var pointOfinterest = await GetPointOfInterestAsync(cityId, pointOfInterestId);
            if(pointOfinterest != null)
            {
                _context.PointOfInterests.Remove(pointOfinterest);
            }
            return pointOfinterest;
        }

        public async Task<(IEnumerable<City>, PaginationMetadata)> GetCitiesAsync(
            string? name, string? searchquery, int pageNumber, int pageSize)
        {
      
            var collection = _context.Cities as IQueryable<City>;
            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = _context.Cities.Where(c => c.Name == name);
            }
            if (!string.IsNullOrWhiteSpace(searchquery))
            {
                searchquery = searchquery.Trim();
                collection = _context.Cities.Where(c => c.Name.Contains(searchquery)
                || (c.Description != null && c.Description.Contains(searchquery)));
            }
            var totalitem = await collection.CountAsync();
            var paginationMetadata = new PaginationMetadata(pageSize, totalitem, pageNumber);
          var finalCollection = await collection.OrderBy(c => c.Name)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();
            return (finalCollection, paginationMetadata);

        }
    }
}
