
namespace TenApplication.Models
{
    public class RaportRecord
    {
        public int RaportRecordId { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double RaportRecordHours { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RaportCreateDate { get; set; }
        
        //RELATIONS
        public int InboxItemId { get; set; }       
        public InboxItem? InboxItem { get; set; }
    }
}
