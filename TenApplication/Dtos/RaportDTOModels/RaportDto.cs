

using TenApplication.Models;

namespace TenApplication.Dtos.RaportDTOModels
{
    public class RaportDto
    {
        public int RaportId { get; set; }
        public DateTime RaportDate { get; set; }
        public double RaportHours { get; set; }
        public List<UserRaportHours>? UserRaportHours { get; set; }
        public List<GroupedRecordsDto>? GroupOfRecords { get; set; }

    }
}
