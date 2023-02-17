using TenApplication.Models;

namespace TenApplication.DTO.CatDTO
{
    public class UpdateCatRecordDto
    {
        public double CatRecordHours { get; set; }
        public DateTime CatRecordCreated { get; set; }
        public int CatId { get; set; }
    }
}
