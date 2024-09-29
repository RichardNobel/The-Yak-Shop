using Microsoft.EntityFrameworkCore;

namespace YakShop.Server.Data.Entities
{
    [Index(nameof(Age))]
    public record HerdMemberEntity(int? Id, string Name, decimal Age, string Sex) { }
}
