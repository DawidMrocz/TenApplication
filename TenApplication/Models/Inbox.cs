

namespace TenApplication.Models
{
    public class Inbox
    {
        public int InboxId { get; set; }
        [Range(0, 100, ErrorMessage = "Task quantity number out of range!")]
        public required int TaskQuantity { get; set; } = 0;
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public required double AllHours { get; set; } = 0;

        //RELATIONS
        public int DesignerId { get; set; }
        public Designer Desinger { get; set; } = null!;
        public List<InboxItem> InboxItems { get; set; } = new List<InboxItem>();

    }
}
