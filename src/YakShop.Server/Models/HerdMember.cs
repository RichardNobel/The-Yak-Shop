using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    // TODO: Add a form of validation (e.g. for the Range attribute on the Age property).
    // Unfortunately this doesn't seem to be supported -yet?- by System.Text.Json deserializer.
    // See https://blog.json-everything.net/posts/deserialization-with-schemas/

    /// <summary>
    /// A single member of the yak herd.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Age">The age is given in standard Yak years (0-10).</param>
    /// <param name="Sex">MALE or FEMALE</param>
    public record HerdMember(
        [Required] string Name,
        [Required, Range(0, 10, ErrorMessage = "Age must be a positive value between 0 and 10.")]
            decimal Age,
        [Required] string Sex
    )
    {
        [JsonIgnore]
        public short DaysAlive => (short)(Age * 100);
    }
}
