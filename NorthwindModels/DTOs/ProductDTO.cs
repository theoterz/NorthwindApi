using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class ProductDTO
    {
        [Key]
        public int ProductId { get; set; }
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
        public bool Discontinued { get; set; }
    }
}
