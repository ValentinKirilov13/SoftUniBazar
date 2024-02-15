using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants;

namespace SoftUniBazar.Data.Models
{
    [Comment("Table of Categories)")]
    public class Category
    {
        [Key]
        [Comment("Category Identifier)")]
        public int Id { get; set; }

        [Required]
        [MaxLength(CategoryNameMaxLenght)]
        [Comment("Category Name)")]
        public string Name { get; set; } = string.Empty;

        public ICollection<Ad> Ads { get; set; } = new List<Ad>();
    }
}