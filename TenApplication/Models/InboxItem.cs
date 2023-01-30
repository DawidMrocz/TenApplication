using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class InboxItem
    {
        public Guid InboxItemId { get; set; }
        [Range(0, 1000,ErrorMessage ="Value out of range!")]
        public double Hours { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int Components { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int DrawingsComponents { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int DrawingsAssembly { get; set; } = 0;
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}