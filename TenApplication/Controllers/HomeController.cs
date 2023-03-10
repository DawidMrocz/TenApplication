using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using TenApplication.ActionFilters;
using TenApplication.Dtos;
using TenApplication.Helpers;
using TenApplication.Repositories;
using TenApplication.Dtos.JobDTOModels;
using Microsoft.AspNetCore.Authorization;
using TenApplication.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace TenApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IJobRepository _jobRepository;
        private readonly ILogger<HomeController> _logger;
        private readonly IDistributedCache _cache;

        public HomeController(
            ILogger<HomeController> logger, 
            IJobRepository jobRepository, 
            IDistributedCache cache
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobRepository = jobRepository ?? throw new ArgumentNullException(nameof(jobRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedList<JobDto>>> Index([FromQuery]QueryParams queryParams)
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
                return View(jobs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }          
        }

        [HttpGet]
        [IdProvidedValidation(IdName = "jobId")]
        public async Task<ActionResult<PaginatedList<JobDto>>> Details([FromRoute]int? jobId)
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
        [ValidateAntiForgeryToken]
        [ValidationFilter(DTOName = "job")]
        public async Task<IActionResult> Create([FromBody][Bind(include: "Name,Surname,BirthDate,Gender,CarLicense")] Job job)
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
        [ValidateAntiForgeryToken]
        [ValidationFilter(DTOName = "job")]
        public async Task<IActionResult> Update([FromBody][Bind(include: "Name,Surname,BirthDate,Gender,CarLicense")] Job job)
        {
            try
            {
                await _jobRepository.Update(job);
                await _cache.DeleteRecordAsync<JobDto>($"job_{job.JobId}");
                await _cache.DeleteRecordAsync<PaginatedList<JobDto>>(JsonConvert.SerializeObject(new QueryParams()));
                return Ok();
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, ex);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return BadRequest("Server error!");
            }
        }

        [HttpDelete(Name = "Delete")]
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
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, ex);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return BadRequest("Server error!");
            }
        }

        [HttpPost]
        [IdProvidedValidation(IdName = "jobId")]
        public async Task<ActionResult> CreateInboxItem([FromRoute] int jobId)
        {
            var authenticatedId = Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            
            try
            {
                await _jobRepository.AddToInbox(jobId, authenticatedId);
                await _cache.DeleteRecordAsync<JobDto>($"job_{jobId}");
                await _cache.DeleteRecordAsync<PaginatedList<JobDto>>(JsonConvert.SerializeObject(new QueryParams()));
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex.Message, ex);
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
                return BadRequest("Server error!");
            }  
        }
    }
}