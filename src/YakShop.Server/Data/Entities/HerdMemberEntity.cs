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
    public record HerdMemberEntity(
        [Required] string Name,
        [Required, Range(0, 10, ErrorMessage = "Age must be a positive value between 0 and 10.")]
            decimal Age,
        [Required] string Sex
    )
    {
        [Key]
        public Guid Id { get; init; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
