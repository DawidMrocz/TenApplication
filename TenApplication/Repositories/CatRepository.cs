using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos.CatDTOModels;

namespace TenApplication.Repositories
{
    public class CatRepository:ICatRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CatRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<CatRecordDto>> GetAll(int userId)
        {
            return await _applicationDbContext.CatRecords
                .Include(rec => rec.Cat)
                .Where(u => u.Cat!.UserId == userId)
                .OrderBy(c => c.Created)
                .Select(c => new CatRecordDto()
                {
                    Created = c.Created,
                })
                .AsNoTracking()               
                .ToListAsync();
        }

        public async Task<CatDto> GetById(int catId)
        {
            CatDto? cat = await _applicationDbContext.Cats
                .Include(d => d.Designer)
                .Include(rec => rec.CatRecords)
                .ThenInclude(i => i.InboxItem)
                .ThenInclude(j => j.Job)
                .Where(u => u.CatId == catId)
                .Select(c => new CatDto()
                {
                    CatId = c.CatId,
                    CCtr = c.Designer!.CCtr,
                    ActTyp = c.Designer.ActTyp,
                    CatRecords = c.CatRecords.Select(c => new CatRecordDto()
                    {
                        CatRecordId = c.CatRecordId,
                        CellHours = c.CellHours,
                        Created = c.Created,
                        Region = c.InboxItem.Job!.Region,
                        ProjectNumber = c.InboxItem.Job.ProjectNumber,
                        ProjectName = c.InboxItem.Job.ProjectName,
                        SapText = c.InboxItem.Job.SapText,
                        Receiver = c.InboxItem.Job.Receiver
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return cat;
        }
    }
}
