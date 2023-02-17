using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.DTO.CatDTO;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class CatRepository:ICatRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public CatRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<Cat>> GetAll(int userId)
        {
            return await _applicationDbContext.Cats
                .Include(rec => rec.CatRecords.Select(d => d.Created))
                .Where(u => u.UserId == userId)
                .Select(c => c.Created)
                .AsNoTracking()
                .OrderBy(c => c.CatCreateDate)
                .ToListAsync();
        }

        public async Task<CatDto> GetById(int catId)
        {
            return await _applicationDbContext.Cats
                .AsSplitQuery()
                .Include(d => d.Designer)
                .Include(rec => rec.CatRecords)
                .ThenInclude(j => j.Job)
                .Where(u => u.CatId == catId)
                .Select(c => new CatDto(){
                    CatId = c.CatId
                    CCtr = c.Designer.CCtr   
                    ActTyp = c.Designer.ActTyp 
                    CatRecords = c.CatRecords.Select(c => new CatRecordDto(){
                        CatRecordId = c.CatRecordId   
                        CellHours = c.CellHours
                        Created = c.Created               
                        Region = c.Job.Region
                        ProjectNumber =c.Job.ProjectNumber 
                        ProjectName = c.Job.ProjectName   
                        SapText = c.Job.SapText
                        Receiver = c.Job.Receiver 
                    }).ToListAsync();
                })
                .AsNoTracking()
                .FirtsOrDefaultAsync();
        }
    }
}
