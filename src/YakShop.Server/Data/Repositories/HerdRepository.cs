﻿using Microsoft.EntityFrameworkCore;
using YakShop.Server.Data.Entities;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Repositories
{
    public interface IHerdRepository : IBaseRepository<Herd>
    {
        void CreateHerd(Herd herd);
        Herd GetHerd();
        void DeleteHerd();
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
                db.HerdMembers.Add(new HerdMemberEntity(member.Name, member.Age, member.Sex));
            }
        }

        public void DeleteHerd()
        {
            db.Database.ExecuteSql($"TRUNCATE TABLE [{nameof(YakShopDbContext.HerdMembers)}]");
        }
    }
}