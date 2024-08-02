using Commerce.ProductAPI.Entity;
using Microsoft.EntityFrameworkCore;


namespace ProductAPI.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           modelBuilder.Entity<Product>().HasData(new Product 
           { 
               ProductId = 1,
               Name = "Samosa",
               Price = 15,
               Description = "gdjhdghjgdjghjh.<br /> jhduhjdhjdhjdhjhdjdjhdjj",
               ImageUrl = "",
               CategoryName = "Appetizer"

           
           
           });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Amala",
                Price = 15.99,
                Description = "gdjhdghjgdjghjh.<br /> jhduhjdhjdhjdhjhdjdjhdjj",
                ImageUrl = "",
                CategoryName = "Desert"



            });
        }

    }
}
