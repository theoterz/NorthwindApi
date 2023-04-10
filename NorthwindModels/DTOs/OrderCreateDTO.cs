using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindModels.DTOs
{
    public class OrderCreateDTO
    {
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
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
