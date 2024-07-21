using DemoCommerce.API.Entity;
using Microsoft.EntityFrameworkCore;
using static DemoCommerce.API.Entity.Coupon;

namespace DemoCommerce.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<Coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CouponConfig());
        }

    }
}
