

namespace TenApplication.Models
{
    public class CatRecord
    {
        public int CatRecordId { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double DayHours { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }

        //RELATIONS
        public int CatId { get; set; }
        public Cat? Cat { get; set; }
        public int InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }
    }
}
