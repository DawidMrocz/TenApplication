

using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Raport
    {
        public int RaportId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RaportCreateDate { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double AllRaportHours { get; set; }
        public List<RaportRecord> RaportRecords { get; set; } = new List<RaportRecord>();
    }
}
