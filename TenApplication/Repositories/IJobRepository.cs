using TenApplication.Dtos;
using TenApplication.Dtos.JobDTOModels;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IJobRepository
    {
        public Task<PaginatedList<JobDto>> GetAll(QueryParams queryParams);
        public Task<JobDto> GetById(int? id);
        public Task Create(Job product);
        public Task Update(Job product);
        public Task Delete(int id);
    }
}
