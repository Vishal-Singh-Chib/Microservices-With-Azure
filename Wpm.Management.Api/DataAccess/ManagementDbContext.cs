using Microsoft.EntityFrameworkCore;

namespace Wpm.Management.Api.DataAccess
{
    public class ManagementDbContext : DbContext
    {
        public ManagementDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Pet> Pets { get; set; }  
        public DbSet<Breed> Breeds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for Breeds
            modelBuilder.Entity<Breed>().HasData( 
                
                new Breed(1, "Beagle"),
                new Breed(2, "Terrier"),
                new Breed(3, "Beagle")
            );

            // Seed data for Pets
            modelBuilder.Entity<Pet>().HasData(
                new Pet { Id = 1, Name = "Bella", Age = 3, BreedId = 1 }, // Beagle
                new Pet { Id = 2, Name = "Charlie", Age = 5, BreedId = 2 }, // Terrier
                new Pet { Id = 3, Name = "Max", Age = 2, BreedId = 1 }, // Beagle
                new Pet { Id = 4, Name = "Lucy", Age = 4, BreedId = 2 } // Terrier
            );
        }
    }

    public static class ManagementDbContextExtext
    {
        public static void EnsureDbISCreated(this IApplicationBuilder app)
        {
            using var scope=app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetService<ManagementDbContext>();
            context!.Database.EnsureCreated();
        }  
    }
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int BreedId { get; set; }

        public Breed Breed { get; set; }
    }

    public record Breed(int  Id, string Name);
}
