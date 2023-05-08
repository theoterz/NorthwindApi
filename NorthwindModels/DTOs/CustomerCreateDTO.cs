﻿using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class CustomerCreateDTO
    {
        [Key]
        [StringLength(5, ErrorMessage = "The field Customer ID must be a string with a maximum length of 5")]
        [Required(ErrorMessage = "The Customer ID field is required.")]
        public string CustomerID { get; set; } = null!;
        [MaxLength(40)]
        [Required(ErrorMessage = "The Company Name field is required.")]
        public string CompanyName { get; set; } = null!;
        [MaxLength(30)]
        public string? ContactName { get; set; }
        [MaxLength(30)]
        public string? ContactTitle { get; set; }
        [MaxLength(60)]
        public string? Address { get; set; }
        [MaxLength(15)]
        public string? City { get; set; }
        [MaxLength(15)]
        public string? Region { get; set; }
        [MaxLength(15)]
        public string? Country { get; set; }
    }
}
