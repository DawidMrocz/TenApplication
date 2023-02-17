using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TenApplication.ActionFilters;
using TenApplication.Dtos;
using TenApplication.Helpers;
using TenApplication.Models;
using TenApplication.Repositories;
using TenApplication.Dtos.JobDTOModels;
using TenApplication.Dtos.InboxDTO;
using System.Security.Claims;

namespace TenApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly IInboxRepository _inboxRepository;
        private readonly ILogger<JobController> _logger;
        private readonly IDistributedCache _cache;

        public JobController(ILogger<JobController> logger, IJobRepository jobRepository, IDistributedCache cache, IInboxRepository inboxRepository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _inboxRepository = inboxRepository ?? throw new ArgumentNullException(nameof(inboxRepository));
        }

        [HttpGet(Name = "jobs")]
        public async Task<ActionResult<PaginatedList<JobDto>>> Get([FromQuery]QueryParams queryParams)
        {          
            try
            {
                string key = JsonConvert.SerializeObject(queryParams);
                PaginatedList<JobDto>? jobs = await _cache.GetRecordAsync<PaginatedList<JobDto>>(key);
                if (jobs is null) 
                {
                    jobs = await _jobRepository.GetAll(queryParams);
                    await _cache.SetRecordAsync(key, jobs);
                }               
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }          
        }

        [HttpGet(Name = "job")]
        [Route("/job/{id}")]
        [IdProvidedValidation(IdName = "jobId")]
        public async Task<ActionResult<PaginatedList<JobDto>>> GetById([FromRoute]int? jobId)
        {
            try
            {
                JobDto? job = await _cache.GetRecordAsync<JobDto>($"job_{jobId}");
                if (job is null)
                {
                    await _jobRepository.GetById(jobId);
                    await _cache.SetRecordAsync($"job_{jobId}", job);
                } 
                if (job is null) return NotFound("job NOT FOUND");
                return Ok(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }

        [HttpPost(Name = "Create")]
        [ValidationFilter(DTOName = "job")]
        public async Task<IActionResult> Create([FromBody][Bind(include:"Name,Surname,BirthDate,Gender,CarLicense")] Job job)
        {
            try
            {
                await _jobRepository.Create(job);
                await _cache.DeleteRecordAsync<JobDto>($"job_{job.JobId}");
                await _cache.DeleteRecordAsync<PaginatedList<JobDto>>(JsonConvert.SerializeObject(new QueryParams()));
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }

        [HttpPut(Name = "Update")]
        [Route("/job/{id}")]
        [ValidateAntiForgeryToken]
        [ValidationFilter(DTOName="job")]
        public async Task<IActionResult> Update([FromBody][Bind(include:"Name,Surname,BirthDate,Gender,CarLicense")] Job job)
        {
            try
            {
                    await _jobRepository.Update(job);
                    await _cache.DeleteRecordAsync<JobDto>($"job_{job.JobId}");
                    await _cache.DeleteRecordAsync<PaginatedList<JobDto>>(JsonConvert.SerializeObject(new QueryParams()));
                    return Ok();
            }
            catch (DbUpdateException ex )
            {
                _logger.LogError(ex.Message, ex);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return BadRequest("Server error!");
            }
        }

        [HttpDelete(Name = "Delete")]
        [Route("/job/{id}")]
        [IdProvidedValidation(IdName = "jobId")]
        public async Task<IActionResult> Delete([FromRoute] int jobId)
        {
            try
            {
                await _jobRepository.Delete(jobId);
                await _cache.DeleteRecordAsync<JobDto>($"job_{jobId}");
                await _cache.DeleteRecordAsync<PaginatedList<JobDto>>(JsonConvert.SerializeObject(new QueryParams()));
                return Ok();
            }
            catch (DbUpdateException ex )
            {
                _logger.LogError(ex.Message, ex);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return BadRequest("Server error!");
            }
        }

        [HttpGet]
        [IdProvidedValidation(IdName = "jobId")]
        public async Task<ActionResult<InboxItemDto>> CreateInboxItem([FromRoute] int jobId)
        {
            var authenticatedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            await _inboxRepository.CreateInboxItem(jobId,authenticatedId);
            return RedirectToAction("Index", "Home");
        }
    }
}