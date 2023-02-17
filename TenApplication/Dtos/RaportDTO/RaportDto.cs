using TenApplication.Models;


namespace TenApplication.DTO.RaportDTO
{
    public class RaportDto
    {
        public int RaportId { get; set; }
        public required DateTime RaportCreateDate { get; set; }
        public List<Designer>? Designers { get; set; }
        public List<InboxItem>? InboxItems { get; set; }
    }
}
