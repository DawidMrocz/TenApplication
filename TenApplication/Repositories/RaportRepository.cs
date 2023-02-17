using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos.RaportDTOModels;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class RaportRepository : IRaportRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RaportRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<RaportRecordDto>> GetAll()
        {
            List<RaportRecordDto>? records = await _applicationDbContext.RaportRecords
                .OrderBy(c => c.RaportCreateDate)
                .AsNoTracking()
                .Select(r => new RaportRecordDto()
                {
                    RaportCreateDate = r.RaportCreateDate
                })             
                .Distinct()             
                .ToListAsync();
            return records;
1       }

        public async Task<RaportRecordDto> GetById(int CatId)
        {
            return await _applicationDbContext.RaportRecords
                .Include(i => i.InboxItem)
                    .ThenInclude(j => j.Job)
                .Include(i => i.InboxItem)
                    .ThenInclude(i => i.Inbox)
                    .ThenInclude(d => d.Desinger)
                    .Select(r => new RaportRecordDto(){
                        RaportCreateDate = r.RaportCreateDate,
                        RaportRecordHours = r.,
                        RaportRecords = r.RaportRecords.Select(rec => new RaportRecordDto(){
                            RaportRecordId = rec.RaportRecordId,
                            RaportRecordHours = rec.RaportRecordHours,
                            Components = rec.InboxItem.Components,
                            DrawingsComponents = rec.InboxItem.DrawingsComponents,
                            DrawingsAssembly = rec.InboxItem.DrawingsAssembly,
                            Name = rec.InboxItem.Inbox.Designer.Name,
                            Surname = rec.InboxItem.Inbox.Designer.Surname,
                            Software = rec.InboxItem.Job.Software,
                            Ecm = rec.InboxItem.Job.Ecm,
                            Gpdm = rec.InboxItem.Job.Gpdm,
                            ProjectNumber = rec.InboxItem.Job.ProjectNumber,
                            Client = rec. InboxItem.Job.Client,        
                            DueDate = rec.InboxItem.Job.DueDate,
                            Started = rec.InboxItem.Job.Started,
                            Finished = rec.InboxItem.Job.Finished
                        }).ToList()
                    });
        }
    }
}