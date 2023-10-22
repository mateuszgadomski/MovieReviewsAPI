using System;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewsAPI.Models
{
    public class UpdateReviewDto
    {
        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [Required]
        public bool IsWorth { get; set; }
    }
}