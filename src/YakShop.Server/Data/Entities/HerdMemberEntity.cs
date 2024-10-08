using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using YakShop.Server.Models;

namespace YakShop.Server.Data.Entities
{
    /// <summary>
    /// A single member of the yak herd.
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Age">The age is given in standard Yak years (0-10).</param>
    /// <param name="Sex">MALE or FEMALE</param>
    [Index(nameof(Age))]
    public record HerdMemberEntity : IHerdMember
    {
        public HerdMemberEntity(string name, decimal age, string sex, decimal ageLastShaved, decimal ageNextShave)
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
            AgeLastShaved = ageLastShaved;
            AgeNextShave = ageNextShave;
            Name = name;
            Sex = sex;
        }

        [Key]
        [Column(Order = 1)]
        public Guid Id { get; init; }

        [Column(Order = 4)]
        [Required, Range(0, 10, ErrorMessage = $"{nameof(Age)} must be a positive value between 0 and 10.")]
        public decimal Age { get; set; }

        [Column(Order = 5)]
        [Required, Range(0, 10, ErrorMessage = $"{nameof(AgeLastShaved)} must be a positive value between 0 and 10.")]
        public decimal AgeLastShaved { get; set; }

        [Column(Order = 6)]
        [Required, Range(1, 10, ErrorMessage = $"{nameof(AgeNextShave)} must be a positive value between 1 and 10.")]
        public decimal AgeNextShave { get; set; }

        [Required]
        [Column(Order = 2)]
        public string Name { get; init; }

        [Required]
        [Column(Order = 3)]
        public string Sex { get; init; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
