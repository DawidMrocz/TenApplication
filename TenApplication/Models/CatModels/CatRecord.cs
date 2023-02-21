

using TenApplication.Models.CatModels;

namespace TenApplication.Models
{
    public class CatRecord
    {
        public Guid CatRecordId { get; set; }    
   
        //RELATIONS        
        public Guid CatId { get; set; }
        public Cat Cat { get; set; } = null!;
        public Guid InboxItemId { get; set; }
        public InboxItem InboxItem { get; set; } = null!;
        public List<CatRecordCell> CatRecordCells { get; set; } = new List<CatRecordCell>();
    }
}
