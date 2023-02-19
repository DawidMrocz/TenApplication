
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models.RaportModels
{
    public class RaportRecord
    {
        public int RaportRecordId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RaportRecordDate { get; set; }
        public int InboxItemId { get; set; }
        public required InboxItem InboxItem { get; set; }
    }
}
