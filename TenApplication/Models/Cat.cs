
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public int CatId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CatCreateDate { get; set; }
        //[Precision(2)]
        //[Range(0, 500, ErrorMessage = "Hours number out of range!")]       
        //public double AllHoursHours { get; set; }
        //[Range(0, 100, ErrorMessage = "Task quantity number out of range!")]
        //public int QuantityOfRecords { get; set; }
        public int UserId { get; set; }
        public Designer? Designer { get; set; }
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}