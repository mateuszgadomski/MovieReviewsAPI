using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class CreateAndUpdateCategoryDto
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}