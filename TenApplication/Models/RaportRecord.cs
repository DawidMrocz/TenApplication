
namespace TenApplication.Models
{
    public class RaportRecord
    {
        public int RaportRecordId { get; set; }
        public double RaportRecordHours { get; set; }
        public int InboxItemId { get; set; }       
        public InboxItem? InboxItem { get; set; }
    }
}
