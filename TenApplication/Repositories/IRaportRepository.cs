using TenApplication.Dtos.RaportDTOModels;

namespace TenApplication.Repositories
{
    public interface IRaportRepository
    {
        public Task<List<RaportRecordDto>> GetAll();
        public Task<List<RaportRecordDto>> GetById(int raportId);
    }
}