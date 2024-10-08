namespace YakShop.Server.Models
{
    public class StockInfo(int dayNumber, decimal milk, int skins)
    {
        public int DayNumber { get; set; } = dayNumber;
        public decimal Milk { get; set; } = milk;
        public int Skins { get; set; } = skins;
    }
}