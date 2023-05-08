using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class OrderCreateDTO
    {
        [StringLength(5, ErrorMessage = "The field Customer ID must be a string with a maximum length of 5")]
        public string? CustomerID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The Employee Id must be greater than 0!")]
        public int? EmployeeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "The Shipper Id must be greater than 0!")]
        public int? ShipVia { get; set; }
        [MaxLength(40)]
        public string? ShipName { get; set; }
        [MaxLength(60)]
        public string? ShipAddress { get; set; }
        [MaxLength(15)]
        public string? ShipCity { get; set; }
        [MaxLength(15)]
        public string? ShipRegion { get; set; }
        [MaxLength(15)]
        public string? ShipCountry { get; set; }
    }
}
