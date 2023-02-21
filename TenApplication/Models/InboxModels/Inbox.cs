

namespace TenApplication.Models
{
    public class Inbox
    {
        public Guid InboxId { get; set; }

        //RELATIONS
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<InboxItem> InboxItems { get; set; } = new List<InboxItem>();
    }
}
