using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    public record Herd
    {
        [JsonPropertyName("herd")]
        public required List<HerdMember> Members { get; init; }
    }
}
