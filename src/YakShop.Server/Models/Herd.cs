using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    public record Herd
    {
        public required List<HerdMember> Members { get; set; }
    }

    public record HerdMember(string Name, byte Age, string Sex)
    {
        [JsonIgnore]
        public short DaysAlive => (short)(Age * 100);
    }
}
