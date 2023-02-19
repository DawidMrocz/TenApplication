using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models.RaportModels
{
    public class Raport
    {
        public int RaportId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RaportDate { get; set; }
        public List<RaportRecord> RaportRecords { get; set;} = new List<RaportRecord>();
    }
}