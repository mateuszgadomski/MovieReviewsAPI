using MovieReviewsAPI.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public string Category { get; set; }
        public List<ReviewDto> Reviews { get; set; }
    }
}