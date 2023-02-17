

namespace TenApplication.Models
{
    public class CatRecord
    {
        public int CatRecordId { get; set; }
        public double DayHours { get; set; }
        public DateTime Day { get; set; }

        //RELATIONS
        public int CatId { get; set; }
        public Cat? Cat { get; set; }
        public int InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }
    }
}
