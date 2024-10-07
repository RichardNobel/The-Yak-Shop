namespace YakShop.Server.Models
{
    public static class StatKey
    {
        public const string ShopOpenDate = "ShopOpenDate";
    }

    public record Stat(string Key, string Value)
    { }
}
