using MovieReviewsAPI.Interfaces;
using System.Collections.Generic;

namespace MovieReviewsAPI.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public virtual List<Review> Reviews { get; set; }
    }
}