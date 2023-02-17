using TenApplication.Models;


namespace TenApplication.DTO.RaportDTO
{
    public class RaportDto
    {
        public int RaportId { get; set; }
        public DateTime RaportCreateDate { get; set; }
        public double AllRaportHours { get; set; }
        public List<RaportRecordDto> RaportRecords { get; set; }
    }
}
