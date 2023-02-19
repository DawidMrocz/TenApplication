

namespace TenApplication.Dtos.RaportDTOModels
{
    public class GroupedRecordsDto
    {
        public required string ClientName { get; set; }
        public double GroupHours { get; set; }
        public List<RaportRecordDto>? RaportRecords { get; set; }
    }
}
