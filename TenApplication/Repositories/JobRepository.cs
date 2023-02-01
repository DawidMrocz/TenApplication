using TenApplication.Data;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
