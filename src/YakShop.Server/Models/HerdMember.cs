using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace YakShop.Server.Models
{
    public interface IHerdMember
    {
        decimal Age { get; set; }
        decimal AgeLastShaved { get; set; }
        decimal AgeNextShave { get; set; }
        string Name { get; init; }
        string Sex { get; init; }

    }

    // TODO: Add a form of validation (e.g. for the Range attribute on the Age property).
    // Unfortunately this doesn't seem to be supported -yet?- by System.Text.Json deserializer.
    // See https://blog.json-everything.net/posts/deserialization-with-schemas/

    /// <summary>
    /// A single member of the yak herd.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Age">The age is given in standard Yak years (0-10).</param>
    /// <param name="Sex">MALE or FEMALE</param>
    public record HerdMember : IHerdMember
    {
        [JsonConstructor]
        public HerdMember(string name, decimal age, string sex)
        {
            if (
                !sex.Equals("MALE", StringComparison.InvariantCultureIgnoreCase)
                && !sex.Equals("FEMALE", StringComparison.InvariantCultureIgnoreCase)
            )
            {
                throw new ArgumentException(
                    "Either MALE or FEMALE should be specified.",
                    nameof(sex)
                );
            }

            Age = age;
            Name = name;
            Sex = sex;
        }

        public HerdMember(Guid? id, string name, decimal age, string sex, decimal ageLastShaved, decimal ageNextShave) : this(name, age, sex)
        {
            Id = id.GetValueOrDefault();
            AgeLastShaved = ageLastShaved;
            AgeNextShave = ageNextShave;
        }

        [JsonIgnore]
        public Guid Id { get; init; }

        [Required]
        public decimal Age { get; set; }

        [JsonIgnore]
        public int AgeInDays => (int)(Age * 100);

        [Required]
        [JsonIgnore]
        public decimal AgeLastShaved { get; set; }

        [JsonIgnore]
        public decimal AgeNextShave { get; set; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Sex { get; init; }
    }
}
