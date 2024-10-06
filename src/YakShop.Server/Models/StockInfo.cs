namespace YakShop.Server.Models
{
    public class StockInfo(decimal milk, int skins)
    {
        public decimal Milk { get; set; } = milk;
        public int Skins { get; set; } = skins;
    }
}
