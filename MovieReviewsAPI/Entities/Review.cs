using System;
using System.Collections.Generic;

namespace MovieReviewsAPI.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsWorth { get; set; }

        public string PublicationDate { get; set; }
            = DateTime.Now.ToString("dd/MM/yyy");

        public int MovieId { get; set; }
        public virtual Movie Movie { get; set; }
    }
}