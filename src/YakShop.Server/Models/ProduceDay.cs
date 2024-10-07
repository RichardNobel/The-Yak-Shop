using System.ComponentModel.DataAnnotations;
using YakShop.Server.Data.Entities;

namespace YakShop.Server.Models
{
    public record ProduceDay([Required] int DayNumber, [Required] decimal Milk, [Required] int Skins)
    {
        public ProduceDay(ProduceDayEntity produceDayEntity) : this(produceDayEntity.DayNumber, produceDayEntity.Milk, produceDayEntity.Skins)
        { }
    }
}