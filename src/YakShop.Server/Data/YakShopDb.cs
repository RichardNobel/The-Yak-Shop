using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;

namespace YakShop.Server.Data
{
    public class YakShopDb(DbContextOptions<YakShopDb> options) : DbContext(options)
    {
        public DbSet<HerdMemberEntity> HerdMembers => Set<HerdMemberEntity>();
        public DbSet<OrderEntity> Orders => Set<OrderEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HerdMemberEntity>(builder =>
            {
                builder.ToTable(nameof(HerdMembers));
            });
        }
    }
}
