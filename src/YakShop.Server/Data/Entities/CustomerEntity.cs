using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record CustomerEntity(string Name)
    {
        [Key]
        public Guid Id { get; init; }

        public List<OrderEntity> Orders { get; init; } = [];

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
