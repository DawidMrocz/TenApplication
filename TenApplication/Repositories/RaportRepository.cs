using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos.RaportDTOModels;
using TenApplication.Models.RaportModels;

namespace TenApplication.Repositories
{
    public class RaportRepository : IRaportRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public RaportRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Raport>> GetAll()
        {
            List<Raport>? raport = await _applicationDbContext.Raports
                .AsNoTracking()
                .OrderBy(r => r.RaportDate)
                .ToListAsync();

            return raport;
        }

        public async Task<RaportDto> GetById(int raportId)
        {
            RaportDto? raport = await _applicationDbContext.Raports
                .AsSingleQuery()
                .Include(rec => rec.RaportRecords)
                    .ThenInclude(i => i.InboxItem)
                        .ThenInclude(j => j.Job)
                .Include(rec => rec.RaportRecords)
                    .ThenInclude(i => i.InboxItem)
                        .ThenInclude(i => i.Inbox)
                            .ThenInclude(d => d.Designer)
                .Select(r => new RaportDto()
                {
                    RaportDate = r.RaportDate,
                    RaportRecords = r.RaportRecords.Select(r => new RaportRecordDto()
                    {
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
                    }).ToList()
                }).FirstOrDefaultAsync(r => r.RaportId == raportId);

            return raport;
        }
    }
}