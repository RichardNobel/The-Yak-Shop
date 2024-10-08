using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IHerdRepository : IBaseRepository<Herd>
    {
        void CreateHerd(Herd herd);

        Herd GetHerd();

        void DeleteHerd();

        void UpdateMember(HerdMember herdMember);
    }

    public class HerdRepository(YakShopDbContext dbContext)
        : BaseRepository<Herd>(dbContext),
            IHerdRepository
    {
        public Herd GetHerd() =>
            new()
            {
                Members = db
                    .HerdMembers.Select(entity => new HerdMember(
                        entity.Id,
                        entity.Name,
                        entity.Age,
                        entity.Sex,
                        entity.AgeLastShaved,
                        entity.AgeNextShave
                    ))
                    .ToArray()
            };

        public void CreateHerd(Herd herd)
        {
            foreach (var member in herd.Members)
            {
                db.HerdMembers.Add(new HerdMemberEntity(member.Name, member.Age, member.Sex, ageLastShaved: member.Age, ageNextShave: member.Age));
            }
        }

        public void DeleteHerd()
        {
            db.HerdMembers.ExecuteDelete();
        }

        public void UpdateMember(HerdMember herdMember)
        {
            var entity = db.HerdMembers.Find(herdMember.Id);
            if (entity == null)
            {
                return;
            }

            entity.Age = herdMember.Age;
            entity.AgeLastShaved = herdMember.AgeLastShaved;

            db.HerdMembers.Update(entity);
            Save();
        }
    }
}