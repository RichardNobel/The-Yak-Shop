using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace YakShop.Server.Data.Entities
{
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
