using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModels.DTOs
{
    public class ProductCreateDTO
    {
        [Required]
        [MaxLength(40)]
        public string ProductName { get; set; } = null!;
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
        [MaxLength(20)]
        public string? QuantityPerUnit { get; set; }
        public decimal? UnitPrice { get; set; }
    }
}
