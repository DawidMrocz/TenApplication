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

        public async Task<List<CatDto>> GetAll(int userId)
        {
            return await _applicationDbContext.Cats
                .Where(c => c.UserId == userId)
                .AsNoTracking()
                .OrderBy(r => r.CatDate)
                .Select(c => new CatDto()
                {
                    CatId= c.CatId,
                    CatDate= c.CatDate
                })
                .ToListAsync();
        }

        public async Task<CatDto> GetById(int catId)
        {
            CatDto? cat = await _applicationDbContext.Cats
                .Include(d => d.Designer)
                .Include(rec => rec.CatRecords)
                    .ThenInclude(i => i.InboxItem)
                        .ThenInclude(j => j.Job)
                .Include(rec => rec.CatRecords)
                    .ThenInclude(i => i.CatRecordCells)
                .Where(u => u.CatId == catId)
                .Select(c => new CatDto()
                {
                    CatId = c.CatId,
                    CatRecords = c.CatRecords.Select(c => new CatRecordDto()
                    {
                        CatRecordId = c.CatRecordId,
                        Region = c.InboxItem!.Job!.Region,
                        CCtr = c.InboxItem.Inbox.User.CCtr,
                        ActTyp = c.InboxItem.Inbox.User.ActTyp,
                        ProjectNumber = c.InboxItem.Job.ProjectNumber,
                        ProjectName = c.InboxItem.Job.ProjectName,
                        SapText = c.InboxItem.Job.SapText,
                        Receiver = c.InboxItem.Job.Receiver,
                        CatRecordCells = c.CatRecordCells.Select(c => new CatRecordCellDto()
                        {
                            CatRecordCellId = c.CatRecordCellId,
                            CellHours = c.CellHours,
                            CatRecordCellDate= c.CatRecordCellDate,
                        }).OrderBy(c => c.CatRecordCellDate).ToList()
                    }).ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync();

            if (cat is null) throw new BadHttpRequestException("Cat do not exist !");

            return cat;
        }
    }
}
