using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IProduceDayRepository : IBaseRepository<ProduceDay>
    {
        void Add(ProduceDay produceDay);
        void DeleteAll();
        ProduceDay? Get(int dayNumber);
        ProduceDay GetLatest();
        (decimal milk, int skins) GetTotalQuantitiesUntilDay(int dayNumber);
    }

    public class ProduceDayRepository(YakShopDbContext dbContext)
        : BaseRepository<ProduceDay>(dbContext),
            IProduceDayRepository
    {
        public void Add(ProduceDay produceDay)
        {
            db.ProduceDays.Add(new ProduceDayEntity(produceDay.Milk, produceDay.Skins));
            Save();
        }

        public void DeleteAll()
        {
            db.ProduceDays.ExecuteDelete();
        }

        public ProduceDay? Get(int dayNumber)
        {
            var produceDayEntity = db.ProduceDays.Find(dayNumber);
            return produceDayEntity == null ? null : new ProduceDay(produceDayEntity);
        }

        public ProduceDay GetLatest()
        {
            var produceDayEntity = db.ProduceDays.OrderByDescending(pd => pd.DayNumber).FirstOrDefault();
            return produceDayEntity == null
                ? throw new InvalidDataException("Could not retrieve latest produce day.")
                : new ProduceDay(produceDayEntity);
        }

        public (decimal milk, int skins) GetTotalQuantitiesUntilDay(int dayNumber)
        {
            var totals = db.ProduceDays.GroupBy(pd => 1).Select(g => new { Milk = g.Sum(pd => pd.Milk), Skins = g.Sum(pd => pd.Skins) }).FirstOrDefault();
            return totals == null ? (0, 0) : (totals.Milk, totals.Skins);
        }
    }
}
