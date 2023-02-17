

namespace TenApplication.DTO.InboxDTO
{
    public class InboxItemDto
    {
        public int InboxItemId { get; set; }
        public double Hours { get; set; } 
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public int Ecm { get; set; }
        public int Gpdm { get; set; }
        public string? JobDescription { get; set; }
        public int Status { get; set; }
        public DateTime? Received { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
        public List<CatRecord> CatRecords { get; set; }
    }
}
