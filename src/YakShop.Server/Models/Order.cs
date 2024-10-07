namespace YakShop.Server.Models
{
    public record Order
    {
        public Order(int dayNumber, int milk, int skins)
        {
            DayNumber = dayNumber;
            Milk = milk;
            Skins = skins;
        }

        public int DayNumber { get; set; }
        public int Milk { get; set; }
        public int Skins { get; set; }

        public Customer? Customer { get; set; } = null;
    }
}
