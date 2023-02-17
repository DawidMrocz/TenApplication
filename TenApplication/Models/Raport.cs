

using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Raport
    {
        public int RaportId { get; set; }
        [DataType(DataType.Date)]
        public DateTime RaportCreateDate { get; set; }
        public double AllRaportHours { get; set; }
        public List<RaportRecord> RaportRecords { get; set; } = new List<RaportRecord>();
    }
}
