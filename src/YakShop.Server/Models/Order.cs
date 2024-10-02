namespace YakShop.Server.Models
{
    public record Order(int Milk, int Skins)
    {
        public Customer? Customer { get; set; } = null;
    }
}
