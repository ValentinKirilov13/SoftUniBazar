using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.Constants.DataConstants;

namespace SoftUniBazar.Data.Models
{
    [Comment("Table of Ads")]
    public class Ad
    {
        [Key]
        [Comment("Ad Identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaxLenght)]
        [Comment("Ad Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(AdDescriptionMaxLenght)]
        [Comment("Ad Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Ad Price")]
        public decimal Price { get; set; }

        [Required]
        [Comment("Ad Owner Identifier")]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        [Comment("Ad Img")]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        [Comment("Ad Created On")]
        public DateTime CreatedOn { get; set; }

        [Required]
        [Comment("Ad Category Identifier")]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
