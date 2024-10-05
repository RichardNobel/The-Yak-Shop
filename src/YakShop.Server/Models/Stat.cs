namespace YakShop.Server.Models
{
    public static class StatKey
    {
        public const string ShopOpenDate = "ShopOpenDate";
        public const string StockSkins = "StockSkins";
        public const string StockMilk = "StockMilk";
    }

    public record Stat(string Key, string Value)
    { }
}
