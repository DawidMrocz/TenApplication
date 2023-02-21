using TenApplication.Models;

namespace TenApplication.Dtos.CatDTOModels
{
    public class UpdateCatRecordDto
    {
        public double CatRecordHours { get; set; }
        public DateTime CatRecordCreated { get; set; }
        public Guid CatId { get; set; }
    }
}
