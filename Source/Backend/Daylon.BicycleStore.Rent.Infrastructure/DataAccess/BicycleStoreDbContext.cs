using Microsoft.EntityFrameworkCore;

namespace Daylon.BicycleStore.Rent.Infrastructure.DataAccess
{
    public class BicycleStoreDbContext : DbContext
    {
        public BicycleStoreDbContext(DbContextOptions<BicycleStoreDbContext> options)
            : base(options)
        {
        }

        public DbSet<Domain.Entity.Bicycle> Bicycles { get; set; } = null!;
        public DbSet<Domain.Entity.RentalOrder> RentalOrders { get; set; } = null!;
        public DbSet<Domain.Entity.User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entity.Bicycle>()
                .ToTable("Bicycles")
                .HasKey(key => key.Id);

            modelBuilder.Entity<Domain.Entity.RentalOrder>()
                .ToTable("RentalOrders")
                .HasKey(key => key.OrderId);

            //==|=====>
            base.OnModelCreating(modelBuilder);
        }
    }
}
