using Microsoft.EntityFrameworkCore;
using NewCityInfo.API.Entities;

namespace NewCityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
       

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new Entities.City("city1")
                {
                    Id = 1,
                    Description = "city description 1"
                },
                new Entities.City("city2")
                {
                Id = 2,
                    Description = "city description 2"
                },
                 new Entities.City("city3")
                 {
                Id = 3,
                    Description = "city description 3"
                }

            );
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("pointOfInterest1")
                {
                    CityId = 1,
                    Id = 1,
                    Description = "point of interest description 1"
                },
                new PointOfInterest("pointOfInterest2")
                {
                    CityId = 1,
                    Id = 2,
                    Description = "point of interest description 2"
                },
                 new PointOfInterest("pointOfInterest3")
                 {
                     CityId = 2,
                     Id = 3,
                     Description = "point of interest description 3"
                 },
                new PointOfInterest("pointOfInterest4")
                {
                    CityId = 2,
                    Id = 4,
                    Description = "point of interest description 4"
                },
                 new PointOfInterest("pointOfInterest5")
                 {
                     CityId = 3,
                     Id = 5,
                     Description = "point of interest description 5"
                 },
                new PointOfInterest("pointOfInterest6")
                {
                    CityId = 3,
                    Id = 6,
                    Description = "point of interest description 6"
                });
            base.OnModelCreating(modelBuilder);
        }

    }
}
