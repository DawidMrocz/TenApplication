using TenApplication.Dtos.CatDTOModels;

namespace TenApplication.Repositories
{
    public interface ICatRepository
    {
        public Task<List<CatDto>> GetAll(Guid userId);
        public Task<CatDto> GetById(Guid CatId);
    }
}
