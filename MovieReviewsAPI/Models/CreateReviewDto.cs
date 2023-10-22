using MovieReviewsAPI.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class CreateReviewDto
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public bool IsWorth { get; set; }
    }
}