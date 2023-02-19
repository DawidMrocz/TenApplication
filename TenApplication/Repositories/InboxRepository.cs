using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos.InboxDTO;
using TenApplication.Models;
using TenApplication.Models.CatModels;
using TenApplication.Models.RaportModels;

namespace TenApplication.Repositories
{
    public class InboxRepository : IInboxRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public InboxRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<InboxDto> GetInbox(int userId)
        {
            return await _applicationDbContext.Inboxs
                .AsNoTracking()
                .Include(d => d.Designer)
                .Include(i => i.InboxItems!)
                .ThenInclude(j => j.Job)
                .Select(i => new InboxDto()
                {
                    InboxId = i.InboxId,
                    TaskQuantity = i.InboxItems!.Count(),
                    AllHours = i.InboxItems!.Sum(h => h.Hours),
                    UserId = i.Designer.UserId,
                    Name = i.Designer.Name,
                    Surname = i.Designer.Surname,
                    Photo = i.Designer.Photo,
                    InboxItems = i.InboxItems!.Select(i => new InboxItemDto()
                    {
                        InboxItemId = i.InboxItemId,
                        Hours = i.Hours,
                        Components = i.Components,
                        DrawingsComponents = i.DrawingsComponents,
                        DrawingsAssembly = i.DrawingsAssembly,
                        Ecm = i.Job!.Ecm,
                        Gpdm = i.Job.Gpdm,
                        JobDescription = i.Job.JobDescription,
                        Status = i.Job.Status,
                        Received = i.Job.Received,
                        DueDate = i.Job.DueDate,
                        Started = i.Job.Started,
                        Finished = i.Job.Finished
                    }).ToList()
                })
                .OrderBy(j => j.InboxItems!.Select(j => j.DueDate))
                .SingleAsync(i => i.UserId == userId);
        }       

        public async Task CreateInboxItem(int jobId,int inboxId)
        {
            InboxItem newInboxItem = new()
            {
                Hours = 0,
                Components = 0,
                DrawingsComponents = 0,
                DrawingsAssembly = 0,
                JobId = jobId,
                InboxId = inboxId,
            };
            await _applicationDbContext.InboxItems.AddAsync(newInboxItem);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task UpdateInboxItem(UpdateInboxItemDto inboxItem,int inboxItemId)
        {
            await _applicationDbContext.InboxItems.Include(j => j.Job)
                .Where(p => p.InboxItemId == inboxItemId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.Hours, b => inboxItem.Hours)
                    .SetProperty(b => b.Components, b => inboxItem.Components)
                    .SetProperty(b => b.DrawingsComponents, b => inboxItem.DrawingsComponents)
                    .SetProperty(b => b.DrawingsAssembly, b => inboxItem.DrawingsAssembly)
                    .SetProperty(b => b.Job!.Status, b => inboxItem.Status)
                    .SetProperty(b => b.Job!.Started, b => inboxItem.Started)
                    .SetProperty(b => b.Job!.Finished, b => inboxItem.Finished)
                    );
        }

        public async Task DeleteInboxItem(int inboxItemId)
        {
            await _applicationDbContext.InboxItems.Where(p => p.InboxItemId == inboxItemId).ExecuteDeleteAsync();
        }

        public async Task AddCatRecord(int inboxItemId, int userId, DateTime entryDate, double hours)
        {           
            Cat? userCat = await _applicationDbContext.Cats
            .Include(rec => rec.CatRecords)
                .ThenInclude(c => c.CatRecordCells)
            .FirstOrDefaultAsync(
                c => c.UserId == userId && 
                c.CatDate == entryDate);

            if(userCat is null)
            {
                userCat = new Cat()
                {
                    CatDate = entryDate,
                    UserId = userId,
                    CatRecords = new List<CatRecord>()
                };
                await _applicationDbContext.Cats.AddAsync(userCat);
            }

            if(!userCat.CatRecords.Any(i => i.InboxItemId == inboxItemId)) {
                CatRecord newRecord = new()
                { 
                    CatId=userCat.CatId,
                    InboxItemId=inboxItemId,
                    CatRecordCells = new List<CatRecordCell>()
                };
                await _applicationDbContext.CatRecords.AddAsync(newRecord);                    
            }
            else
            {              
                CatRecord? userCatRecord = await _applicationDbContext.CatRecords.FirstOrDefaultAsync(rec => rec.CatId == userCat.CatId && rec.Created == entryDate);

                if(userCatRecord is null){
                    userCat.CatRecords.Add(newCatRecord);
                    RaportRecord raportRecord = new()
                    {
                        RaportCreateDate = entryDate,
                        InboxItemId = inboxItemId
                    };
                    await _applicationDbContext.RaportRecords.AddAsync(raportRecord);     
                }
                else {
                    userCatRecord.CellHours = hours;      
                }                
            };
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteCatRecord(int inboxItemId,int catRecordId, int userId, DateTime entryDate)
        {    
            Cat? userCat = await _applicationDbContext.Cats
            .Include(rec => rec.CatRecords)
            .FirstOrDefaultAsync(
                c => c.UserId == userId && 
                c.CatRecords.Any(rec => rec.Created == entryDate));

            CatRecord? recordToDelete = userCat!.CatRecords.FirstOrDefault(r => r.InboxItemId == inboxItemId && r.Created.Equals(entryDate));

            _applicationDbContext.CatRecords.Remove(recordToDelete);

            if(userCat.CatRecords.Where(r => r.InboxItemId == inboxItemId).Count() == 0){
                await _applicationDbContext.RaportRecords.Where(p => p.InboxItemId== inboxItemId).ExecuteDeleteAsync();
            }
        }
    }
}
