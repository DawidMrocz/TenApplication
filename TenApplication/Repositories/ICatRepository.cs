using TenApplication.Dtos.CatDTOModels;

namespace TenApplication.Repositories
{
    public interface ICatRepository
    {
        public Task<List<CatRecordDto>> GetAll(int userId);
        public Task<CatDto> GetById(int CatId);
    }
}
