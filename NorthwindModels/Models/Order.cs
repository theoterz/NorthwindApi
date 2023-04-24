﻿using System.ComponentModel.DataAnnotations;

namespace NorthwindModels.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        [StringLength(5)]
        public string? CustomerID { get; set; }
        public int? EmployeeID { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        [MaxLength(40)]
        public string? ShipName { get; set; }
        [MaxLength(60)]
        public string? ShipAddress { get; set; }
        [MaxLength(15)]
        public string? ShipCity { get; set; }
        [MaxLength(15)]
        public string? ShipRegion { get; set; }
        [MaxLength(10)]
        public string? ShipPostalCode { get; set; }
        [MaxLength(15)]
        public string? ShipCountry { get; set; }
    }
}
