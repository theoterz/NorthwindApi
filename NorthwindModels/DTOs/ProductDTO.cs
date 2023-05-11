using NorthwindModels.ErrorMessages;
using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public record ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required(ErrorMessage = ProductErrorMessages.ProductNameRequired)]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;
        [Range(0,int.MaxValue, ErrorMessage = ProductErrorMessages.SupplierIdRange)]
        public int? SupplierId { get; set; }
        [Range(0,int.MaxValue,ErrorMessage = ProductErrorMessages.CategoryIdRange)]
        public int? CategoryId { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        [Range(0.0, double.MaxValue, ErrorMessage = ProductErrorMessages.UnitPriceRange)]
        public decimal? UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
