using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record CustomerEntity
    {
        [Key]
        public Guid Id { get; init; }
        public string Name { get; init; } = string.Empty;

        public List<OrderEntity> Orders { get; init; } = [];

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
