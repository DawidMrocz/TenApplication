using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TenApplication.ActionFilters;
using TenApplication.DTO;
using TenApplication.Dtos;
using TenApplication.Helpers;
using TenApplication.Models;
using TenApplication.Repositories;
using System.Net;

namespace TenApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<JobController> _logger;
        private readonly IDistributedCache _cache;

        public JobController(ILogger<JobController> logger, IJobRepository jobRepository, IDistributedCache cache)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet(Name = "jobs")]
        [ProducesResponseType(typeof(PaginatedList<JobDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
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
        [ProducesResponseType(typeof(JobDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [IdProvidedValidation]
        public async Task<ActionResult<PaginatedList<JobDto>>> GetById([FromRoute]int? id)
        {
            try
            {
                JobDto? job = await _cache.GetRecordAsync<JobDto>($"job_{id}");
                if (job is null)
                {
                    await _jobRepository.GetById(id);
                    await _cache.SetRecordAsync($"job_{id}", job);
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] Job job)
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
        [IdProvidedValidation]
        [ValidationFilter(DTOName="job")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] Job job, [FromRoute]int? id)
        {
            try
            {
                    await _jobRepository.Update(job);
                    await _cache.DeleteRecordAsync<JobDto>($"job_{id}");
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
        [IdProvidedValidation]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                await _jobRepository.Delete(id);
                await _cache.DeleteRecordAsync<JobDto>($"job_{id}");
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
        [IdProvidedValidation]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<InboxItemDTO>> CreateInboxItem([FromRoute] int jobId)
        {
            await _inboxRepository.CreateInboxItem(jobId);
            return RedirectToAction("Index", "Home");
        }
    }
}