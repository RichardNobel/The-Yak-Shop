using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record ProduceDayEntity
    {
        public ProduceDayEntity(decimal milk, int skins)
        {
            Milk = milk;
            Skins = skins;
        }

        [Key]
        public int DayNumber { get; init; }

        [Required]
        public decimal Milk { get; init; }

        [Required]
        public int Skins { get; init; }
    }
}
