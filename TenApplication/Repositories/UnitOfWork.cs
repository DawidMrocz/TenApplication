using TenApplication.Data;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        public IJobRepository Jobs { get; }
        public UnitOfWork(ApplicationDbContext dbContext,
                            IJobRepository jobs)
        {
            _dbContext = dbContext;
            Jobs = jobs;
        }

        public async Task Save()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
