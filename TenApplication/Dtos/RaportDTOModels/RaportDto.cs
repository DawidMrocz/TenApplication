

namespace TenApplication.Dtos.RaportDTOModels
{
    public class RaportDto
    {
        public int RaportId { get; set; }
        public DateTime RaportDate { get; set; }
        public List<RaportRecordDto> RaportRecords { get; set; } = new List<RaportRecordDto>();
    }
}
