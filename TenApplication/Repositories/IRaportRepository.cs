using TenApplication.Dtos.RaportDTOModels;
using TenApplication.Models.RaportModels;

namespace TenApplication.Repositories
{
    public interface IRaportRepository
    {
        public Task<List<Raport>> GetAll();
        public Task<List<RaportRecordDto>> GetById(int raportId);
    }
}