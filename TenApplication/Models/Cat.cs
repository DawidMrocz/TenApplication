
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public int CatId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CatCreateDate { get; set; }
        public int UserId { get; set; }
        public Designer? Designer { get; set; }
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}