namespace TenApplication.Dtos.InboxDTO
{
    public class UpdateInboxItemDto
    {
        public double Hours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }
        public int Status { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
    }
}
