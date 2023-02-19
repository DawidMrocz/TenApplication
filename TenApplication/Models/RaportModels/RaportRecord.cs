
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models.RaportModels
{
    public class RaportRecord
    {
        public int RaportRecordId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RaportRecordDate { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double RaportRecordHours { get; set; }
        public int InboxItemId { get; set; }
        public InboxItem InboxItem { get; set; } = null!;
    }
}
