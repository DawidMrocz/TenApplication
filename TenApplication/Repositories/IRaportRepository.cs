using TenApplication.DTO;
using TenApplication.DTO.CatDTO;
using TenApplication.DTO.InboxDTO;
using TenApplication.Dtos;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IRaportRepository
    {
        public Task<List<Raport>> GetAll(int userId);
        public Task<Raport> GetById(int raportId);

        public Task CreateRaport(int inboxItemId, int catId);
        public Task UpdateRaport(UpdateCatRecordDto catRecord);
        public Task DeleteRaport(int catRecordId);

        public Task CreateRaportRecord(int inboxItemId, int catId);
        public Task UpdateRaportRecord(UpdateCatRecordDto catRecord);
        public Task DeleteRaportRecord(int catRecordId);
    }
}