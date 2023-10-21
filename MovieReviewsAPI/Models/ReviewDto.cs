using Microsoft.EntityFrameworkCore.Storage;
using MovieReviewsAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class ReviewDto
    {
        [Required]
        [MinLength(20)]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public bool IsWorth { get; set; }

        public string PublicationDate { get; set; }
            = DateTime.Now.ToString("dd/MM/yyy");
    }
}