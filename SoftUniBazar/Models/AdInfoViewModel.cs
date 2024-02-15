namespace SoftUniBazar.Models
{
    public class AdInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string CreatedOn { get; set; } = string.Empty;

        public string Category { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Price { get; set; } = string.Empty;

        public string Owner { get; set; } = string.Empty;
    }
}
