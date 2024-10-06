using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    internal interface IStatRepository : IBaseRepository<Stat>
    {
        StockInfo GetCurrentStockStats();
        string GetValue(string key);
        void SetValue(string key, string value);
    }

    public class StatRepository(YakShopDbContext dbContext) : BaseRepository<Stat>(dbContext), IStatRepository
    {
        public StockInfo GetCurrentStockStats()
        {
            var stats = db.Stats.Where(s => s.Key.Contains("Stock")).ToList();
            _ = decimal.TryParse(stats.SingleOrDefault(s => s.Key == "StockMilk")?.Value, out decimal milkAmount);
            _ = int.TryParse(stats.SingleOrDefault(s => s.Key == "StockSkins")?.Value, out int skinsAmount);
            return new StockInfo(milkAmount, skinsAmount);
        }

        public string GetValue(string key)
        {
            return db.Stats.FirstOrDefault(s => s.Key == key)?.Value
                ?? throw new ArgumentException($"Stat item with key '{key}' does not exist", nameof(key));
        }

        public void SetValue(string key, string value)
        {
            var statEntity = db.Stats.FirstOrDefault(s => s.Key == key) ?? new StatEntity(key, value);
            if (statEntity.Id == Guid.Empty)
            {
                db.Stats.Add(statEntity);
            }
            else
            {
                statEntity.Value = value;
                db.Stats.Update(statEntity);
            }

            _ = Save();
        }
    }
}
