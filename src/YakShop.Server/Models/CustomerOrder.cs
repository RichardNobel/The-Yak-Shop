using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    public record CustomerOrder
    {
        [JsonPropertyName("customer")]
        public required string CustomerName { get; init; }

        public required Order Order { get; init; }
    }
}