
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class InboxItem
    {
        public int InboxItemId { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double Hours { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int Components { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int DrawingsComponents { get; set; } = 0;
        [Range(0, 1000, ErrorMessage = "Value out of range!")]
        public int DrawingsAssembly { get; set; } = 0;

        //RELATIONS
        public int JobId { get; set; }
        public Job? Job { get; set; }
        public int InboxId { get; set; }
        public Inbox? Inbox { get; set; }
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}