using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.DTOs
{
    public class OrderDTO
    {
        [Key]
        public int OrderID { get; set; }
        [StringLength(5)]
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
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

        /// <summary>
        /// Creates a shallow copy of the object
        /// </summary>
        /// <returns>A copy of the object</returns>
        public OrderDTO Clone()
        {
            return (OrderDTO)MemberwiseClone();
        }
    }
}
