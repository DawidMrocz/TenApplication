

namespace TenApplication.Models
{
    public class Inbox
    {
        public int InboxId { get; set; }

        //RELATIONS
        public int UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public List<InboxItem> InboxItems { get; set; } = new List<InboxItem>();
    }
}
