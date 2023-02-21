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
        public async Task<InboxDto> GetInbox(Guid userId)
        {
            InboxDto? inbox = await _applicationDbContext.Inboxs
                .AsNoTracking()
                .Include(d => d.User)
                .Include(i => i.InboxItems!)
                .ThenInclude(j => j.Job)
                .Select(i => new InboxDto()
                {
                    InboxId = i.InboxId,
                    TaskQuantity = i.InboxItems!.Count(),
                    AllHours = i.InboxItems!.Sum(h => h.Hours),
                    UserId = i.User.Id,
                    UserName = i.User.UserName,
                    Photo = i.User.Photo,
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
                .FirstOrDefaultAsync(i => i.UserId == userId);

                if(inbox is null) throw new BadHttpRequestException("Inbox do not exist!");

                return inbox;
        }            

        public async Task UpdateInboxItem(UpdateInboxItemDto inboxItem,Guid inboxItemId)
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

        public async Task DeleteInboxItem(Guid inboxItemId)
        {
            await _applicationDbContext.InboxItems.Where(p => p.InboxItemId == inboxItemId).ExecuteDeleteAsync();
        }

        public async Task SendHours(Guid inboxItemId, Guid userId, DateTime entryDate, double hours)
        {
            Cat? userCat = await _applicationDbContext.Cats
            .Include(rec => rec.CatRecords)
                .ThenInclude(c => c.CatRecordCells)
            .FirstOrDefaultAsync(
                c => c.UserId == userId &&
                c.CatDate == entryDate);

            if (userCat is null)
            {
                userCat = new Cat()
                {
                    CatDate = entryDate,
                    UserId = userId,
                    CatRecords = new List<CatRecord>()
                };
                await _applicationDbContext.Cats.AddAsync(userCat);
            }

            CatRecord? userCatRecord = userCat.CatRecords.FirstOrDefault(r => r.InboxItemId == inboxItemId);

            if (userCatRecord is null) {
                userCatRecord = new CatRecord()
                {
                    CatId = userCat.CatId,
                    InboxItemId = inboxItemId,
                    CatRecordCells = new List<CatRecordCell>()
                };
                await _applicationDbContext.CatRecords.AddAsync(userCatRecord);
            }

            CatRecordCell? userCatRecordCell = userCatRecord.CatRecordCells.FirstOrDefault(c => c.CatRecordCellDate == entryDate);

            if(userCatRecordCell is null)
            {
                userCatRecordCell = new CatRecordCell()
                {
                    CatRecordCellDate = entryDate,
                    CatRecordId = userCatRecord.CatRecordId
                };
                await _applicationDbContext.CatRecordCells.AddAsync(userCatRecordCell);
            }
                
            userCatRecordCell.CellHours = hours;

            if (userCatRecord.InboxItem!.Job!.Region != Region.NA) return;

            Raport? raport = await _applicationDbContext.Raports
                .Include(r => r.RaportRecords)
                .FirstOrDefaultAsync(d => d.RaportDate == entryDate);

            if(raport is null)
            {
                raport = new Raport()
                {
                    RaportDate = entryDate,
                };
                await _applicationDbContext.Raports.AddAsync(raport);
            }

            RaportRecord? raportRecord = raport.RaportRecords.FirstOrDefault(i => i.InboxItemId == inboxItemId);

            if(raportRecord is null)
            {
                raportRecord = new RaportRecord()
                {
                    RaportRecordDate = entryDate,
                    InboxItemId = inboxItemId
                };  
            }

            raportRecord.RaportRecordHours = userCatRecord.CatRecordCells.Sum(h => h.CellHours);

            if(raportRecord.RaportRecordHours == 0 && raportRecord.InboxItem.Job!.Region == Region.NA)
            {
                _applicationDbContext.RaportRecords.Remove(raportRecord);
            }
         
            if(raport.RaportRecords.Count == 0)
            {
                await _applicationDbContext.Raports.Where(p => p.RaportId == raport.RaportId).ExecuteDeleteAsync();
            }

            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
