using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.DTO.CatDTO;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class CatRepository:ICatRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RaportRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Raport>> GetAll()
        {
            return await _applicationDbContext.Raports
                .Select(r => r.RaportCreateDate)
                .AsNoTracking()
                .OrderBy(c => c.RaportCreateDate)
                .ToListAsync();
        }

        public async Task<Raport> GetById(int CatId)
        {
            Raport? result = await _applicationDbContext.Raports
                .Include(r => r.RaportRecords)
                    .ThenInclude(i => i.InboxItem)
                    .Select(r => r)
                    .Select(i => new InboxItemDto(){
                        InboxItemId=
                        Components=
                        DrawingsComponents=
                        DrawingsAssembly=
                        Ecm=
                        Gpdm=
                        DueDate=
                        Started=
                        Finished=
                    })
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