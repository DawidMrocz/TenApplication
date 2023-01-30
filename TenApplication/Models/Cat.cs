using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public Guid CatRecordId { get; set; }
        [DataType(DataType.Date)]
        public DateTime CatCreateDate { get; set; }
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public double Hours { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public Guid InboxItemId { get; set; }
        public InboxItem? InboxItem { get; set; }
    }
}