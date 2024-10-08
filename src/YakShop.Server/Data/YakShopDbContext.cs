using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;

namespace YakShop.Server.Data
{
    public class YakShopDbContext(DbContextOptions<YakShopDbContext> options) : DbContext(options)
    {
        public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
        public DbSet<HerdMemberEntity> HerdMembers => Set<HerdMemberEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();
        public DbSet<ProduceDayEntity> ProduceDays => Set<ProduceDayEntity>();
        public DbSet<StatEntity> Stats => Set<StatEntity>();

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
                builder.Property(hm => hm.AgeLastShaved).HasColumnType("decimal(4,2)");
                builder.Property(hm => hm.AgeNextShave).HasColumnType("decimal(4,2)");
            });

            modelBuilder.Entity<OrderEntity>(builder =>
            {
                builder.ToTable(nameof(Orders));
                builder.HasOne(o => o.Customer).WithMany(c => c.Orders);
            });

            modelBuilder.Entity<ProduceDayEntity>(builder =>
            {
                builder.ToTable(nameof(ProduceDays));
                builder.HasKey(pd => pd.DayNumber);
                builder.Property(pd => pd.DayNumber).ValueGeneratedNever();
                builder.Property(pd => pd.Milk).HasColumnType("decimal(4,2)");
            });

            modelBuilder.Entity<StatEntity>(builder =>
            {
                builder.ToTable(nameof(Stats));
            });
        }
    }
}