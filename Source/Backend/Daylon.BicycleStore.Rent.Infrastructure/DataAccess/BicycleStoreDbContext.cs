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
    }

}
