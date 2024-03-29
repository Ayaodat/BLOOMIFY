﻿using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class Review
    {
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Review message is required")]
        public string ReviewMessage { get; set; }

        [Range(0, 5, ErrorMessage = "Review rate must be between 0 and 5")]
        public int ReviewRate { get; set; }

        [Required(ErrorMessage = "Review date is required")]
        public DateTime ReviewDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        [Required(ErrorMessage = "Testimonial status is required")]
        public string ReviewStatus { get; set; }

        [Required(ErrorMessage = "User ID is required")]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
