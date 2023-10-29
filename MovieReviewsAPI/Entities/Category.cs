using System.Collections.Generic;

namespace MovieReviewsAPI.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? CreatedById { get; set; }
        public virtual User CreatedBy { get; set; }

        public virtual List<Movie> Movies { get; set; }
    }
}