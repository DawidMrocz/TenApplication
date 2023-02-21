﻿using System.ComponentModel.DataAnnotations;
using TenApplication.Models;

namespace TenApplication.Dtos.CatDTOModels
{
    public class UpdateCatDto
    {
        public int CatId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CatCreateDate { get; set; }
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public double AllHoursHours { get; set; }
        public int QuantityOfRecords { get; set; }

        //RELATIONS
        public int UserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
        public List<CatRecord>? CatRecords { get; set; }
    }
}