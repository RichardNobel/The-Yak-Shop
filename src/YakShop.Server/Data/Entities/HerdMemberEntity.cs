using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace YakShop.Server.Data.Entities
{
    /// <summary>
    /// A single member of the yak herd.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Age">The age is given in standard Yak years (0-10).</param>
    /// <param name="Sex">MALE or FEMALE</param>
    [Index(nameof(Age))]
    public record HerdMemberEntity
    {
        public HerdMemberEntity(string name, decimal age, string sex)
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

        [Key]
        public Guid Id { get; init; }

        [Required, Range(0, 10, ErrorMessage = "Age must be a positive value between 0 and 10.")]
        public decimal Age { get; set; }

        [Required]
        public string Name { get; init; }

        [Required]
        public string Sex { get; init; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
