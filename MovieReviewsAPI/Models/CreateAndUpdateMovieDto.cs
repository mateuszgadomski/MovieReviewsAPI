using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class CreateAndUpdateMovieDto
    {
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(55)]
        public string Author { get; set; }

        public string CategoryName { get; set; }
    }
}