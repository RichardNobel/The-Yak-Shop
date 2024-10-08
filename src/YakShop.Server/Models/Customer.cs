namespace YakShop.Server.Models
{
    public record Customer(string Name)
    {
        public Guid Id { get; init; }
        public List<Order> Orders { get; init; } = [];
    }
}