
namespace TenApplication.Models
{
    public class RaportRecord
    {
        public int RaportRecordId { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double RaportRecordHours { get; set; }
        public int InboxItemId { get; set; }       
        public InboxItem? InboxItem { get; set; }
    }
}
