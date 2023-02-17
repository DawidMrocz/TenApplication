using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IRaportRepository
    {
        public Task<List<RaportRecord>> GetAll();
        public Task<RaportRecord> GetById(int raportId);
    }
}