using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public class StatEntity(string key, string value)
    {
        [Key]
        public Guid Id { get; init; }

        public string Key { get; init; } = key;
        public string Value { get; set; } = value;

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
