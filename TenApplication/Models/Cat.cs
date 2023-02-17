﻿
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public int CatId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CatCreateDate { get; set; }
        [Range(0, 1000, ErrorMessage = "Value out of range!")]        
        public double AllHoursHours { get; set; }
        public int QuantityOfRecords { get; set; }
        public int UserId { get; set; }
        public Designer? Designer { get; set; }
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}