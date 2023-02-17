using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.DTO.InboxDTO;
using TenApplication.Models;
using TenApplicationt.DTO.InboxDTO;

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
                .AsSplitQuery()
                .AsNoTracking()
                .Include(d => d.Desinger)
                .Include(i => i.InboxItems!)
                .ThenInclude(j => j.Job)
                .Select(i => new InboxDto()
                {
                    InboxId = i.InboxId,
                    TaskQuantity = i.InboxItems!.Count(),
                    AllHours = i.InboxItems!.Sum(h => h.Hours),
                    UserId = i.Desinger.UserId,
                    Name = i.Desinger.Name,
                    Surname = i.Desinger.Surname,
                    Photo = i.Desinger.Photo,
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

    }
}
