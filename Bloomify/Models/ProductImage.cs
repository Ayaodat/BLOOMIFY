﻿using System.ComponentModel.DataAnnotations;

namespace Bloomify.Models
{
    public class ProductImage
    {
        [Key]
        public int ImageId { get; set; }

        [Required(ErrorMessage = "Product ID is required")]
        public int ProductId { get; set; }


        public Product Product { get; set; }

        [Required(ErrorMessage = "Image name is required")]
        public string ImageName { get; set; }

        [Required(ErrorMessage = "Content type is required")]
        public string ContentType { get; set; }

        [Required(ErrorMessage = "Image data is required")]
        public byte[] Image { get; set; }
    }
}
