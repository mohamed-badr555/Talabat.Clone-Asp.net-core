﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price Must be greater than zero!")]
        public decimal Price { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        [Range(1, int.MaxValue,ErrorMessage = "Quantity must be greater than one")]
        public int Quantity { get; set; }
    }
}