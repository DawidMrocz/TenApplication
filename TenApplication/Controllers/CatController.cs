using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using TenApplication.Helpers;
using TenApplication.Repositories;
using TenApplication.Dtos.CatDTOModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TenApplication.Controllers
{
    [Authorize]
    public class CatController : Controller
    {
        private readonly ILogger<InboxController> _logger;
        private readonly ICatRepository _catRepository;
        private readonly IDistributedCache _cache;

        public CatController(
            ICatRepository catRepository,
            ILogger<InboxController> logger,
            IDistributedCache cache
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _catRepository = catRepository ?? throw new ArgumentNullException(nameof(catRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        public async Task<ActionResult<List<CatDto>>> Index()
        {
            int authenticatedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            try
            {
                string key = $"Cat_user_{authenticatedId}";
                List<CatDto>? cats = await _cache.GetRecordAsync<List<CatDto>>(key);
                if (cats is null)
                {
                    cats = await _catRepository.GetAll(authenticatedId);
                    await _cache.SetRecordAsync(key, cats);
                }
                return View(cats);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }

        [HttpGet]
        public async Task<ActionResult<CatDto>> Details(int id)
        {
            int authenticatedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            try
            {
                string key = $"Cat_user_{authenticatedId}";
                CatDto? cat = await _cache.GetRecordAsync<CatDto>(key);
                if (cat is null)
                {
                    cat = await _catRepository.GetById(id);
                    await _cache.SetRecordAsync(key, cat);
                }
                return View(cat);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }
    }
}
