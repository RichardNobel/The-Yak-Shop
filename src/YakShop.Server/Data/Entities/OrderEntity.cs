﻿using System.ComponentModel.DataAnnotations;

namespace YakShop.Server.Data.Entities
{
    public record OrderEntity(Guid CustomerId, int Milk, int Skins)
    {
        [Key]
        public Guid Id { get; init; }

        public required CustomerEntity Customer { get; init; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
