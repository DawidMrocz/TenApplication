using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using TenApplication.Dtos.CatDTOModels;
using TenApplication.Dtos.RaportDTOModels;
using TenApplication.Helpers;
using TenApplication.Models.RaportModels;
using TenApplication.Repositories;

namespace TenAppliRaportion.Controllers
{
    public class RaportController : Controller
    {
        private readonly ILogger<RaportController> _logger;
        private readonly IRaportRepository _raportRepository;
        private readonly IDistributedCache _cache;

        public RaportController(
            IRaportRepository raportRepository,
            ILogger<RaportController> logger,
            IDistributedCache cache
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _raportRepository = raportRepository ?? throw new ArgumentNullException(nameof(raportRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        public async Task<ActionResult<List<Raport>>> Index()
        {
            int authentiRaportedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            try
            {
                string key = $"Raport_user_{authentiRaportedId}";
                List<Raport>? raports = await _cache.GetRecordAsync<List<Raport>>(key);
                if (raports is null)
                {
                    raports = await _raportRepository.GetAll();
                    await _cache.SetRecordAsync(key, raports);
                }
                return View(raports);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }

        [HttpGet]
        public async Task<ActionResult<RaportDto>> Details(Guid id)
        {
            Guid authentiRaportedId = Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
            try
            {
                string key = $"Raport_user_{authentiRaportedId}";
                RaportDto? Raport = await _cache.GetRecordAsync<RaportDto>(key);
                if (Raport is null)
                {
                    Raport = await _raportRepository.GetById(id);
                    await _cache.SetRecordAsync(key, Raport);
                }
                return View(Raport);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return NotFound("Server error!");
            }
        }
    }
}
