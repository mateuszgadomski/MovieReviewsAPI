using Microsoft.EntityFrameworkCore.Storage;
using MovieReviewsAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class ReviewDto
    {
        public string Content { get; set; }
        public bool IsWorth { get; set; }
        public string Movie { get; set; }

        public string PublicationDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}