using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IHerdRepository : IDisposable
    {
        Herd GetHerd();
        void CreateHerd(Herd herd);
        void DeleteHerd();
        void Save();
    }

    public class HerdRepository(YakShopDb dbContext) : IHerdRepository
    {
        private readonly YakShopDb db = dbContext;

        public Herd GetHerd() =>
            new()
            {
                Members = db
                    .HerdMembers.Select(entity => new HerdMember(
                        entity.Name,
                        entity.Age,
                        entity.Sex
                    ))
                    .ToList()
            };

        public void CreateHerd(Herd herd)
        {
            foreach (var member in herd.Members)
            {
                db.HerdMembers.Add(new HerdMemberEntity(null, member.Name, member.Age, member.Sex));
            }
        }

        public void DeleteHerd()
        {
            db.Database.ExecuteSql($"TRUNCATE TABLE [{nameof(YakShopDb.HerdMembers)}]");
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                db.Dispose();
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
