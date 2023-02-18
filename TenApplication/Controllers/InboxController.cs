using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Security.Claims;
using TenApplication.ActionFilters;
using TenApplication.Dtos.InboxDTO;
using TenApplication.Helpers;
using TenApplication.Models;
using TenApplication.Repositories;

namespace TenApplication.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        private readonly ILogger<InboxController> _logger;
        private readonly IInboxRepository _inboxRepository;
        private readonly IDistributedCache _cache;

        public InboxController(
            IInboxRepository inboxRepository,
            ICatRepository catRepository,
            IRaportRepository raportRepository,
            ILogger<InboxController> logger,
            IDistributedCache cache
            )
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _inboxRepository = inboxRepository ?? throw new ArgumentNullException(nameof(inboxRepository));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        }

        [HttpGet]
        public async Task<ActionResult<InboxDto>> Inbox()
        {
            string key = $"Inbox_user_{int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value)}";
            InboxDto? userInbox = await _cache.GetRecordAsync<InboxDto>(key);
                if (userInbox is null)
                {
                        userInbox = await _inboxRepository.GetInbox(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
                        await _cache.SetRecordAsync(key, userInbox);               
                }         
            return View(userInbox);
        }       

        [HttpPost]
        [IdProvidedValidation(IdName = "inboxItemId")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<InboxItem>> UpdateInboxItem([FromForm] UpdateInboxItemDto updateInboxItemDto,int inboxItemId,DateTime entryDate)
        {       
            var authenticatedId = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _inboxRepository.UpdateInboxItem(updateInboxItemDto, inboxItemId);
            return RedirectToAction("Inbox", "Inbox");
        }

        [HttpDelete]
        [IdProvidedValidation(IdName = "inboxItemId")]
        public async Task<ActionResult<bool>> DeleteInboxItem([FromRoute] int inboxItemId)
        {
            await _inboxRepository.DeleteInboxItem(inboxItemId);

            return Ok(true);
        }
    }
}