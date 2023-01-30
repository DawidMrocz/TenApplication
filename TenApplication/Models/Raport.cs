using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Raport
    {
        public Guid RaportId { get; set; }
        [DataType(DataType.Date)]
        public required DateTime RaportCreateDate { get; set; }
        public List<User>? Users { get; set; }
        public List<InboxItem>? InboxItems { get; set; }
    }
}
