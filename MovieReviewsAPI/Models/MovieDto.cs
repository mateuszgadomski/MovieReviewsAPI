using MovieReviewsAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class MovieDto
    {
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(55)]
        public string Author { get; set; }

        public string Category { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}