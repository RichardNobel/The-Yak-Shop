namespace YakShop.Server.Models
{
    public record Order(int DayNumber, int Milk, int Skins)
    {
        public Customer? Customer { get; set; } = null;
    }
}
