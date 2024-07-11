using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }

        //DbSets
        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            //Seed Data
            //For Difficulties
            var difficulties = new List<Difficulty>()
            {
                new Difficulty()
                {
                    Id = Guid.Parse("d336f35c-04cb-4835-bbc4-2244dd562167"),
                    Name = "Easy"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("3902bb29-ab41-4c37-9520-d063a30dc0b3"),
                    Name = "Medium"
                },
                new Difficulty()
                {
                    Id = Guid.Parse("e2d524b9-2339-4508-af70-de216e493f0b"),
                    Name = "Hard"
                }
            };

            //For Regions
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id = Guid.Parse("223ce0c9-f8ca-41ba-b5f8-5635b227b095"),
                    Name = "Auckland",
                    Code = "AKL",
                    RegionImageUrl = "https://www.pexels.com/photo/people-walking-on-brown-field-9048793/"
                },
                new Region()
                {
                    Id = Guid.Parse("3e3f725e-ed6c-4fbc-8424-40c3a023b376"),
                    Name = "Northland",
                    Code = "NTL",
                    RegionImageUrl = "https://www.pexels.com/photo/people-walking-on-brown-field-9048793/"
                },
                new Region()
                {
                    Id = Guid.Parse("308a3076-c03a-4882-ab25-b4ffa45eaf0d"),
                    Name = "Bay of Plenty",
                    Code = "BOP",
                    RegionImageUrl = null
                },
                new Region()
                {
                    Id = Guid.Parse("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"),
                    Name = "Wellington",
                    Code = "WGN",
                    RegionImageUrl = null
                },
                new Region
                {
                    Id = Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Name = "Nelson",
                    Code = "NSN",
                    RegionImageUrl = "https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region
                {
                    Id = Guid.Parse("f077a22e-4248-4bf6-b564-c7cf4e250263"),
                    Name = "Southland",
                    Code = "STL",
                    RegionImageUrl = null
                }
            };

            //Seed to the database
            modelBuilder.Entity<Difficulty>().HasData(difficulties);
            modelBuilder.Entity<Region>().HasData(regions);
        }
    }
}
