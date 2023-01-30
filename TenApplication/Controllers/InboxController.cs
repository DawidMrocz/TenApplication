using Aplikacja.DTOS.InboxDtos;
using Aplikacja.DTOS.UserDtos;
using Aplikacja.Entities.InboxModel;
using Aplikacja.Repositories.CatRepository;
using Aplikacja.Repositories.RaportRepository;
using InboxMicroservice.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Aplikacja.Controllers
{
    [Authorize]
    public class InboxController : Controller
    {
        private readonly IInboxRepository _inboxRepository;
        private readonly ICatRepository _catRepository;
        private readonly IRaportRepository _raportRepository;

        public InboxController(IInboxRepository inboxRepository, ICatRepository catRepository, IRaportRepository raportRepository)
        {
            _inboxRepository = inboxRepository ?? throw new ArgumentNullException(nameof(inboxRepository));
            _catRepository = catRepository ?? throw new ArgumentNullException(nameof(catRepository));
            _raportRepository = raportRepository ?? throw new ArgumentNullException(nameof(raportRepository));
        }

        [HttpGet]
        public async Task<ActionResult<UserDto>> Inbox()
        {
            var myInbox = await _inboxRepository.GetMyInbox(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));
            return View(myInbox);
        }

        [HttpGet]
        public async Task<ActionResult<InboxItemDTO>> UpdateInboxItem([FromRoute] Guid inboxItemId)
        {
            InboxItemDTO myInboxItem = await _inboxRepository.GetMyInboxItem(inboxItemId);
            return Ok(myInboxItem);
        }

        [HttpPost]
        public async Task<ActionResult<InboxItem>> UpdateInboxItem([FromForm] UpdateInboxItemDto updateInboxItemDto,Guid inboxItemId,DateTime entryDate)
        {       
            var authenticatedId = Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _inboxRepository.UpdateInboxItem(updateInboxItemDto, inboxItemId);
            var catItem = await _catRepository.CreateCat(authenticatedId, inboxItemId,entryDate);
            await _raportRepository.CreateRaport(authenticatedId, catItem, inboxItemId, entryDate);
            return RedirectToAction("Inbox", "Inbox");
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteInboxItem([FromRoute] Guid inboxItemId)
        {
            bool myInboxItem = await _inboxRepository.DeleteInboxItem(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value), inboxItemId);

            return Ok(true);
        }
    }
}
