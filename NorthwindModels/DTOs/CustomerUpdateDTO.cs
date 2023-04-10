using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModels.DTOs
{
    public class CustomerUpdateDTO
    {
        [Key]
        [StringLength(5)]
        public string CustomerID { get; set; } = null!;
        [MaxLength(40)]
        public string CompanyName { get; set; } = null!;
        [MaxLength(30)]
        public string? ContactName { get; set; }
        [MaxLength(30)]
        public string? ContactTitle { get; set; }
        [MaxLength(60)]
        public string? Address { get; set; }
        public string? Phone { get; set; }
        [MaxLength(24)]
        public string? Fax { get; set; }
    }
}
