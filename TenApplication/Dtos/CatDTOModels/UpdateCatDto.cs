using System.ComponentModel.DataAnnotations;
using TenApplication.Models;

namespace TenApplication.Dtos.CatDTOModels
{
    public class UpdateCatDto
    {
        public Guid CatId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CatCreateDate { get; set; }
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public double AllHoursHours { get; set; }
        public int QuantityOfRecords { get; set; }

        //RELATIONS
        public Guid UserId { get; set; }
        public User? ApplicationUser { get; set; }
        public List<CatRecord>? CatRecords { get; set; }
    }
}