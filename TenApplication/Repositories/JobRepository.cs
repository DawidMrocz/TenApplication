using Microsoft.EntityFrameworkCore;
using TenApplication.Data;
using TenApplication.Dtos;
using TenApplication.Dtos.JobDTOModels;
using TenApplication.Models;
using TenApplication.Dtos.DesignerDTOModels;

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
                .Include(i => i.InboxItems!.Select(i => i.Inbox!.User))
                .AsNoTracking()
                .AsQueryable();

            if (queryParams.SearchBy != null) queryParams.PageNumber = 1;

            if (!String.IsNullOrEmpty(queryParams.SearchBy)) query = query.Where(
                    s => s.Received.ToString()!.Contains(queryParams.SearchBy) ||
                    s.DueDate.ToString()!.Contains(queryParams.SearchBy) ||
                    s.Engineer!.Contains(queryParams.SearchBy) ||
                    s.InboxItems!.Any(d => d.Inbox!.User.UserName.Contains(queryParams.SearchBy))
                );

            if (queryParams.Ecm is not null) query = query.Where(s => s.Ecm == queryParams.Ecm);

            if (queryParams.Client is not null) query = query.Where(s => s.Client == queryParams.Client);

            if (queryParams.Engineer is not null) query = query.Where(s => s.Engineer == queryParams.Engineer);

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
                EngineerName = p.Engineer,
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
                .Include(i => i.InboxItems!.Select(i => i.Inbox!.User))
                .Select(p => new JobDto()
                {
                    JobId = p.JobId,
                    JobDescription = p.JobDescription,
                    TaskType = p.TaskType,
                    Software = p.Software,
                    Link = p.Link,
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
                    Users = p.InboxItems!.Select(i => new UserDto()
                    {
                        UserName = i.Inbox!.User.UserName,
                        Photo = i.Inbox!.User.Photo,
                        Level = i.Inbox!.User.Level

                    }).OrderBy(d => d.UserName).ToList()
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
                    .SetProperty(b => b.Engineer, b => job.Engineer)
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


        //WRITE FUNCTION TO ADD TO INBOX ! 
        public async Task AddToInbox(int jobId, Guid userId)
        {
            Inbox? inbox = await _applicationDbContext.Inboxs.FirstOrDefaultAsync(i => i.UserId == userId);

            if(inbox is null) throw new BadHttpRequestException("Inbox do not exist!");

            InboxItem newInboxItem = new()
            {
                Hours = 0,
                Components = 0,
                DrawingsComponents = 0,
                DrawingsAssembly = 0,
                JobId = jobId,
                InboxId = inbox.InboxId,
            };

            await _applicationDbContext.InboxItems.AddAsync(newInboxItem);
            await _applicationDbContext.SaveChangesAsync();
        }
    }
}
