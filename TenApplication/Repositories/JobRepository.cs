using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.DTO;
using TenApplication.DTO.DesignerDTO;
using TenApplication.Dtos;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public class JobRepository: IJobRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public JobRepository(ApplicationDbContext applicationDbContext)
        { 
            _applicationDbContext = applicationDbContext;
        }

        public async Task<PaginatedList<JobDto>> GetAll(QueryParams queryParams)
        {
            IQueryable<Job> query = _applicationDbContext.Jobs
                .Include(e => e.Engineer!.DisplayName)
                .Include(i => i.InboxItems!.Select(i => i.Inbox!.Desinger))
                .AsNoTracking()
                .AsQueryable();

            if (queryParams.SearchBy != null) queryParams.PageNumber = 1;

            if (!String.IsNullOrEmpty(queryParams.SearchBy)) query = query.Where(
                    s => s.Received.ToString().Contains(queryParams.SearchBy) ||
                    s.DueDate.ToString().Contains(queryParams.SearchBy) ||
                    s.Engineer!.DisplayName.Contains(queryParams.SearchBy) ||
                    s.InboxItems!.Any(d => d.Inbox!.Desinger.Name.Contains(queryParams.SearchBy)) ||
                    s.InboxItems!.Any(d => d.Inbox!.Desinger.Surname.Contains(queryParams.SearchBy))
                );

            if (queryParams.Ecm is not null) query = query.Where(s => s.Ecm == queryParams.Ecm);

            if (queryParams.Client is not null) query = query.Where(s => s.Client == queryParams.Client);

            if (queryParams.Engineer is not null) query = query.Where(s => s.Engineer!.DisplayName == queryParams.Engineer);

            switch (queryParams.SortBy.ToString())
            {
                case "Date_ASC":                   
                    query = query.OrderBy(s => s.Received);
                    break;
                case "Date_DSC":
                    query = query.OrderByDescending(s => s.Received);
                    break;
                default:
                    query = query.OrderBy(s => s.Received);
                    break;
            }

            IQueryable<JobDto> queryDto = query.Select(p => new JobDto()
            {
                JobId = p.JobId,
                TaskType = p.TaskType,
                Software = p.Software,
                EngineerName = p.Engineer!.DisplayName,
                Client = p.Client,
                Status = p.Status,
                Received = p.Received,
                DueDate = p.DueDate,
            }).AsQueryable();

            return await PaginatedList<JobDto>.CreateAsync(queryDto, queryParams.PageNumber ?? 1, 10);
        }

        public async Task<JobDto> GetById(int? id)
        {
            JobDto? Job = await _applicationDbContext.Jobs
                .Include(i => i.InboxItems!.Select(i => i.Inbox!.Desinger))
                .Include(e => e.Engineer!.DisplayName)
                .Select(p => new JobDto()
                {
                    JobId = p.JobId,
                    JobDescription = p.JobDescription,
                    TaskType = p.TaskType,
                    Software = p.Software,
                    Link = p.Link,
                    EngineerName = p.Engineer!.DisplayName,
                    Ecm = p.Ecm,
                    Gpdm = p.Gpdm,
                    Region = p.Region,
                    ProjectNumber = p.ProjectNumber,
                    Client = p.Client,
                    ProjectName = p.ProjectName,
                    Status = p.Status,
                    Received = p.Received,
                    DueDate = p.DueDate,
                    Started = p.Started,
                    Finished = p.Finished,
                    Designers = p.InboxItems!.Select(i => new DesignerDto()
                    {
                        Name = i.Inbox!.Desinger.Name,
                        Surname = i.Inbox!.Desinger.Surname,
                        Photo = i.Inbox!.Desinger.Photo,
                        Level = i.Inbox!.Desinger.Level

                    }).OrderBy(d => d.Name).ToList()
                })
                .AsNoTracking()
                .SingleAsync(p => p.JobId == id);
            
            return Job;
        }

        public async Task Create(Job Job)
        {
                await _applicationDbContext.Jobs.AddAsync(Job);
                await _applicationDbContext.SaveChangesAsync();
        }
       
        public async Task Update(Job job)
        {
            await _applicationDbContext.Jobs
                .Where(p => p.JobId == job.JobId)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(b => b.JobDescription, b => job.JobDescription)
                    .SetProperty(b => b.TaskType, b => job.TaskType)
                    .SetProperty(b => b.Software, b => job.Software)
                    .SetProperty(b => b.Link, b => job.Link)
                    .SetProperty(b => b.EngineerId, b => job.EngineerId)
                    .SetProperty(b => b.Ecm, b => job.Ecm)
                    .SetProperty(b => b.Gpdm, b => job.Gpdm)
                    .SetProperty(b => b.Region, b => job.Region)
                    .SetProperty(b => b.ProjectNumber, b => job.ProjectNumber)
                    .SetProperty(b => b.Client, b => job.Client)
                    .SetProperty(b => b.ProjectName, b => job.ProjectName)
                    .SetProperty(b => b.Status, b => job.Status)
                    .SetProperty(b => b.Received, b => job.Received)
                    .SetProperty(b => b.DueDate, b => job.DueDate)
                    .SetProperty(b => b.Started, b => b.Started)
                    .SetProperty(b => b.Finished, b => b.Finished)
                    );
        }

        public async Task Delete(int id)
        {
            await _applicationDbContext.Jobs.Where(p => p.JobId == id).ExecuteDeleteAsync();
        }
    }
}
