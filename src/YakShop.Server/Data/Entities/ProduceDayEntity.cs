using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record ProduceDayEntity
    {
        public ProduceDayEntity(int dayNumber, decimal milk, int skins)
        {
            DayNumber = dayNumber;
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
