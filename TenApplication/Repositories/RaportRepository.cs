using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos.RaportDTOModels;

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
                .AsNoTracking()
                .Select(r => new RaportRecordDto()
                {
                    RaportCreateDate= r.RaportCreateDate
                })
                .OrderBy(d => d.RaportCreateDate)
                .ToListAsync();

            return records;
        }

        public async Task<List<RaportRecordDto>> GetById(int CatId)
        {
            List<RaportRecordDto>? records = await _applicationDbContext.RaportRecords
                .Include(i => i.InboxItem)
                    .ThenInclude(j => j.Job)
                .Include(i => i.InboxItem)
                    .ThenInclude(i => i.Inbox)
                        .ThenInclude(d => d.Designer)
                .Include(i => i.InboxItem)
                    .ThenInclude(i => i.CatRecords)
                    .Select(r => new RaportRecordDto(){
                        RaportCreateDate = r.RaportCreateDate,
                        RaportRecordHours = r.InboxItem!.CatRecords.Sum(h => h.CellHours),
         
                            RaportRecordId = r.RaportRecordId,
                            Components = r.InboxItem.Components,
                            DrawingsComponents = r.InboxItem.DrawingsComponents,
                            DrawingsAssembly = r.InboxItem.DrawingsAssembly,
                            Name = r.InboxItem.Inbox.Designer.Name,
                            Surname = r.InboxItem.Inbox.Designer.Surname,
                            Software = r.InboxItem.Job.Software,
                            Ecm = r.InboxItem.Job.Ecm,
                            Gpdm = r.InboxItem.Job.Gpdm,
                            ProjectNumber = r.InboxItem.Job.ProjectNumber,
                            Client = r.InboxItem.Job.Client,        
                            DueDate = r.InboxItem.Job.DueDate,
                            Started = r.InboxItem.Job.Started,
                            Finished = r.InboxItem.Job.Finished
            
                    }).ToListAsync();

            return records;
        }
    }
}