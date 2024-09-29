using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    public record HerdMember(string Name, decimal Age, string Sex)
    {
        [JsonIgnore]
        public short DaysAlive => (short)(Age * 100);
    }
}
