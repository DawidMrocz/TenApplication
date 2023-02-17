using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.DTO.CatDTO;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class CatRepository:ICatRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CatRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Cat>> GetAll(int userId)
        {
            return await _applicationDbContext.Cat
                .Where(u => u.UserId == userId)
                .AsNoTracking()
                .OrderBy(c => c.CatCreateDate)
                .ToListAsync();
        }

        public async Task<Cat> GetById(int CatId)
        {
            Cat? result = await _applicationDbContext.Cat.AsNoTracking().SingleOrDefaultAsync(c => c.CatId == CatId);
            return result;
        }

        public async Task CreateCatRecord(int inboxItemId, int catId)
        {
            CatRecord newCatRecord = new()
            {
                DayHours = 0,
                Day = DateTime.Now,
                CatId = catId,
                InboxItemId = inboxItemId
            };
            await _applicationDbContext.CatRecords.AddAsync(newCatRecord);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteCatRecord(int catRecordId)
        {
            await _applicationDbContext.CatRecords.Where(p => p.CatRecordId == catRecordId).ExecuteDeleteAsync();
        }

        public async Task UpdateCatRecord(UpdateCatRecordDto catRecord)
        {
            await _applicationDbContext.CatRecords
                .Where(p => p.Day == catRecord.CatRecordCreated && p.CatId == catRecord.CatId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.DayHours, b => catRecord.CatRecordHours)
                    );
        }
    }
}
