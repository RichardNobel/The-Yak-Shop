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

    public class HerdRepository(YakShopDbContext dbContext) : IHerdRepository
    {
        private readonly YakShopDbContext _db =
            dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public Herd GetHerd() =>
            new()
            {
                Members = _db
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
                _db.HerdMembers.Add(new HerdMemberEntity(member.Name, member.Age, member.Sex));
            }
        }

        public void DeleteHerd()
        {
            _db.Database.ExecuteSql($"TRUNCATE TABLE [{nameof(YakShopDbContext.HerdMembers)}]");
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                _db.Dispose();
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
