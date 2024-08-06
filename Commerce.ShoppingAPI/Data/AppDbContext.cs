


using Commerce.ShoppingAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace ShoppingAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<CartHeader> CartHeaders { get; set; }
        public DbSet<CartDetails> CartDetails { get; set; }


       

    }
}
