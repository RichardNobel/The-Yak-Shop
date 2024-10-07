using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Data.Repositories;
using YakShop.Server.Helpers;
using YakShop.Server.Models;

namespace YakShop.Server.Data
{
    public interface IDbInitializer
    {
        void Initialize(YakShopDbContext context, IProduceDayRepository produceDayRepo);
    }

    public class DbInitializer : IDbInitializer
    {
        public void Initialize(YakShopDbContext context, IProduceDayRepository produceDayRepo)
        {
            context.Database.Migrate();

            if (context.Stats.Any())
            {
                return; // DB has been seeded
            }

            context.Stats.Add(new StatEntity(StatKey.ShopOpenDate, DateTime.Now.ToString()));

            var herdMembers = new HerdMemberEntity[]
            {
                new("Yak-1", age: 4, "FEMALE", ageLastShaved: 4),
                new("Yak-2", age: 8, "FEMALE", ageLastShaved: 8),
                new("Yak-3", age: (decimal)9.5, "FEMALE", ageLastShaved: (decimal)9.5),
            };

            foreach (HerdMemberEntity hm in herdMembers)
            {
                context.HerdMembers.Add(hm);
            }

            // "The moment you open up the Yak Shop webshop will be day 0, and all yaks are eligible to be shaven,
            //  as the two of you spent quite a lot of time setting up this shop and the shepherd wasn't able to
            //  attend much to his herd."
            //
            // For this reason the shop starts with [number of yaks] amount of skins/fur coats initially.
            var initialStockSkins = herdMembers.Length;

            var initialStockMilk = YakProduceCalculator.TotalHerdLitersOfMilkToday(herdMembers);

            produceDayRepo.Add(new ProduceDay(0, initialStockMilk, initialStockSkins));

            context.SaveChanges();
        }
    }
}
