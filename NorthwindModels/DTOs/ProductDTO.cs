using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "The Product Name field is required.")]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;
        [Range(0,int.MaxValue, ErrorMessage = "The Supplier Id should be greater than 0!")]
        public int? SupplierId { get; set; }
        [Range(0,int.MaxValue,ErrorMessage ="The Category Id should be greater than 0!")]
        public int? CategoryId { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = "The Unit Price should be greater than 0!")]
        public decimal? UnitPrice { get; set; }
        public bool Discontinued { get; set; }

        /// <summary>
        /// Creates a shallow copy of the object
        /// </summary>
        /// <returns>A copy of the object</returns>
        public ProductDTO Clone()
        {
            return (ProductDTO)MemberwiseClone();
        }
    }
}
