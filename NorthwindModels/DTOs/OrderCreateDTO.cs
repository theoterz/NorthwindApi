using NorthwindModels.ErrorMessages;
using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public record OrderCreateDTO
    {
        [StringLength(5, ErrorMessage = CustomerErrorMessages.BadIdLength)]
        public string? CustomerID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = OrderErrorMessages.InvalidEmployeeId)]
        public int? EmployeeID { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = OrderErrorMessages.InvalidShipperId)]
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
