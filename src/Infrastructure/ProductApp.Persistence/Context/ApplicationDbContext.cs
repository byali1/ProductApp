using Microsoft.EntityFrameworkCore;
using ProductApp.Domain.Entities;

namespace ProductApp.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public static async Task InitializeDatabaseAsync(ApplicationDbContext context)
        {
            await context.SeedDataAsync();
        }
        private async Task SeedDataAsync()
        {
            if (Products.Any())
                return;

            await Products.AddRangeAsync(new List<Product>
            {
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Laptop",
                    Price = 1500.99M,
                    Quantity = 50,
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Keyboard",
                    Price = 29.99M,
                    Quantity = 300,
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Mouse",
                    Price = 19.99M,
                    Quantity = 500,
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Monitor",
                    Price = 220.49M,
                    Quantity = 120,
                    CreatedDate = DateTime.UtcNow
                },
                new Product
                {
                    Id = Guid.NewGuid(),
                    Name = "Headset",
                    Price = 59.99M,
                    Quantity = 150,
                    CreatedDate = DateTime.UtcNow
                }
            });

            await SaveChangesAsync();
        }

    }
}
