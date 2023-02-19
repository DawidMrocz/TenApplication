

using TenApplication.Models.CatModels;

namespace TenApplication.Models
{
    public class CatRecord
    {
        public int CatRecordId { get; set; }    
   
        //RELATIONS        
        public int CatId { get; set; }
        public Cat Cat { get; set; } = null!;
        public int InboxItemId { get; set; }
        public InboxItem InboxItem { get; set; } = null!;
        public List<CatRecordCell> CatRecordCells { get; set; } = new List<CatRecordCell>();
    }
}
