using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record OrderEntity([Required] Guid CustomerId, [Required] int Milk, [Required] int Skins)
    {
        [Key]
        public Guid Id { get; init; }

        public CustomerEntity Customer { get; init; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
