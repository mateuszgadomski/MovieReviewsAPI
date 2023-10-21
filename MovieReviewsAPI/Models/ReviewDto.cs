using Microsoft.EntityFrameworkCore.Storage;
using MovieReviewsAPI.Entities;
using System;

namespace MovieReviewsAPI.Models
{
    public class ReviewDto
    {
        public string Content { get; set; }
        public bool IsWorth { get; set; }

        public string PublicationDate { get; set; }
            = DateTime.Now.ToString("dd/MM/yyy");
    }
}