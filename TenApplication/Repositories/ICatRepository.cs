using TenApplication.DTO;
using TenApplication.DTO.CatDTO;
using TenApplication.DTO.InboxDTO;
using TenApplication.Dtos;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface ICatRepository
    {
        public Task<List<Cat>> GetAll(int userId);
        public Task<CatDto> GetById(int CatId);
    }
}
