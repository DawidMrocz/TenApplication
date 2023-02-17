

namespace TenApplication.Models
{
    public class Inbox
    {
        public int InboxId { get; set; }

        //RELATIONS
        public int DesignerId { get; set; }
        public Designer Desinger { get; set; } = null!;
        public List<InboxItem> InboxItems { get; set; } = new List<InboxItem>();
    }
}
