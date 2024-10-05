using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;

namespace YakShop.Server.Data
{
    public interface IDbInitializer
    {
        void Initialize(YakShopDbContext context);
    }

    public class DbInitializer : IDbInitializer
    {
        public void Initialize(YakShopDbContext context)
        {
            context.Database.Migrate();

            // Look for any students.
            if (context.HerdMembers.Any())
            {
                return; // DB has been seeded
            }

            var herdmembers = new HerdMemberEntity[]
            {
                new("Yak-1", 4, "FEMALE"),
                new("Yak-2", 8, "FEMALE"),
                new("Yak-3", (decimal)9.5, "FEMALE"),
            };

            foreach (HerdMemberEntity hm in herdmembers)
            {
                context.HerdMembers.Add(hm);
            }

            context.SaveChanges();
        }
    }
}
