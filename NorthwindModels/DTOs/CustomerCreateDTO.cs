using NorthwindModels.ErrorMessages;
using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public record CustomerCreateDTO
    {
        [Key]
        [StringLength(5, ErrorMessage = CustomerErrorMessages.BadIdLength)]
        [Required(ErrorMessage = CustomerErrorMessages.IdRequired)]
        public string CustomerID { get; set; } = null!;
        [MaxLength(40)]
        [Required(ErrorMessage = CustomerErrorMessages.CompanyNameRequired)]
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
