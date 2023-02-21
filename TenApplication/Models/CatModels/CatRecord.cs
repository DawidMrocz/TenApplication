

using TenApplication.Models.CatModels;

namespace TenApplication.Models
{
    public class CatRecord
    {
        public Guid CatRecordId { get; set; }    
   
        //RELATIONS        
        public Guid? CatId { get; set; }
        public Cat? Cat { get; set; }
        public Guid? InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }
        public List<CatRecordCell>? CatRecordCells { get; set; }
    }
}
