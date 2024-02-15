using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SoftUniBazar.Data.Models
{
    [Comment("Table of Ad Buyers")]
    public class AdBuyer
    {
        [Required]
        [Comment("Buyer Identifier")]
        public string BuyerId { get; set; } = string.Empty;

        [ForeignKey(nameof(BuyerId))]
        public IdentityUser Buyer  { get; set; } = null!;

        [Required]
        [Comment("Ad Identifier")]
        public int AdId { get; set; }

        [ForeignKey(nameof(AdId))]
        public Ad Ad { get; set; } = null!;
    }
}

