namespace YakShop.Server.Models
{
    public record Herd
    {
        public required List<HerdMember> Members { get; set; }
    }
}
