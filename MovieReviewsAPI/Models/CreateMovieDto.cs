using MovieReviewsAPI.Entities;
using System.Collections.Generic;

namespace MovieReviewsAPI.Models
{
    public class CreateMovieDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string CategoryName { get; set; }
    }
}