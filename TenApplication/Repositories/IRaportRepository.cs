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
    }
}