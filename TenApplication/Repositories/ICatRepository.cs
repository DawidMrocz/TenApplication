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
        public Task<Cat> GetById(int CatId);
        public Task CreateCatRecord(int inboxItemId, int catId);
        public Task UpdateCatRecord(UpdateCatRecordDto catRecord);
        public Task DeleteCatRecord(int catRecordId);
    }
}
