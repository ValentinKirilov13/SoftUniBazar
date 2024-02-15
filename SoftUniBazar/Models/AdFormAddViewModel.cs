using System.ComponentModel.DataAnnotations;
using static SoftUniBazar.Data.Constants.DataConstants;
using static SoftUniBazar.Models.ErrorMessages.ErrorMessages;

namespace SoftUniBazar.Models
{
    public class AdFormAddViewModel
    {
        [Required(ErrorMessage = RequierdErrorMessage)]
        [StringLength(AdNameMaxLenght, MinimumLength = AdNameMinLenght,ErrorMessage = LengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequierdErrorMessage)]
        [StringLength(AdDescriptionMaxLenght, MinimumLength = AdDescriptionMinLenght, ErrorMessage = LengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequierdErrorMessage)]
        public string ImageUrl { get; set; } = string.Empty;

        [Required(ErrorMessage = RequierdErrorMessage)]
        public decimal Price { get; set; } 

        [Required(ErrorMessage = RequierdErrorMessage)]
        public int CategoryId { get; set; }

        public ICollection<CategoryInfoViewModel> Categories { get; set; } = new List<CategoryInfoViewModel>();
    }
}
