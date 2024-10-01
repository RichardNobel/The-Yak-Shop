namespace YakShop.Server.Models
{
    public record Order(Guid CustomerId, int Milk, int Skins)
    {
        public required Customer Customer { get; set; }
    }
}
