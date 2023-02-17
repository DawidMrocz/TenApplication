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
                .AsSplitQuery()
                .Include(r => r.RaportRecords)
                    .ThenInclude(i => i.InboxItem.Job)
                .Include(r => r.RaportRecords)
                    .ThenInclude(i => i.InboxItem.Inbox.User)
                .Include(r => r.RaportRecords)
                    .ThenInclude(i => i.InboxItem.CatRecords.Where(crec => crec.InboxItemId == ).Select(crec => crec.Hours))
                    .Select(r => new RaportDto(){
                        RaportId = r.RaportId
                        RaportCreateDate = r.RaportCreateDate
                        AllRaportHours = r.AllRaportHours
                        RaportRecords = r.RaportRecords.Select(rec => new RaportRecordDto(){
                            RaportRecordId = rec.RaportRecordId
                            RaportRecordHours = rec.RaportRecordHours
                            Components = rec.InboxItem.Components
                            DrawingsComponents = rec.InboxItem.DrawingsComponents
                            DrawingsAssembly = rec.InboxItem.DrawingsAssembly
                            Name = rec.InboxItem.Inbox.Designer.Name
                            Surname = rec.InboxItem.Inbox.Designer.Surname
                            Software = rec.InboxItem.Job.Software
                            Ecm = rec.InboxItem.Job.Ecm
                            Gpdm = rec.InboxItem.Job.Gpdm
                            ProjectNumber = rec.InboxItem.Job.ProjectNumber
                            Client = rec. InboxItem.Job.Client         
                            DueDate = rec.InboxItem.Job.DueDate
                            Started = rec.InboxItem.Job.Started
                            Finished = rec.InboxItem.Job.Finished
                        })
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