using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class CustomerDTO
    {
        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; } = null!;
        [MaxLength(40)]
        [Required]
        public string CompanyName { get; set; } = null!;
        [MaxLength(30)]
        public string? ContactName { get; set; }
        [MaxLength(30)]
        public string? ContactTitle { get; set; }
        [MaxLength(60)]
        public string? Address { get; set; }
        [MaxLength(15)]
        public string? City { get; set; }
        public string? Phone { get; set; }
        [MaxLength(24)]
        public string? Fax { get; set; }

        /// <summary>
        /// Creates a shallow copy of the object
        /// </summary>
        /// <returns>A copy of the object</returns>
        public CustomerDTO Clone()
        {
            return (CustomerDTO)MemberwiseClone();
        }
    }
}
