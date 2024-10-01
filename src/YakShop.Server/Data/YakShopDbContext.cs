using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;

namespace YakShop.Server.Data
{
    public class YakShopDbContext(DbContextOptions<YakShopDbContext> options) : DbContext(options)
    {
        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<HerdMemberEntity> HerdMembers => Set<HerdMemberEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerEntity>(builder =>
            {
                builder.ToTable(nameof(Customers));
                builder.HasMany(c => c.Orders).WithOne(o => o.Customer);
            });

            modelBuilder.Entity<HerdMemberEntity>(builder =>
            {
                builder.ToTable(nameof(HerdMembers));
                builder.Property(hm => hm.Age).HasColumnType("decimal(4,2)");
            });

            modelBuilder.Entity<OrderEntity>(builder =>
            {
                builder.ToTable(nameof(Orders));
                builder.HasOne(o => o.Customer).WithMany(c => c.Orders);
            });
        }
    }
}
