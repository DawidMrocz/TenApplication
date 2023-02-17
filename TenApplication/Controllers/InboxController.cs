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
            ILogger<HomeController> logger,
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
            Inbox? userInbox = await _cache.GetRecordAsync<InboxDto>(key);
                if (userInbox is null)
                {
                        userInbox = await _inboxRepository.GetInbox(int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value));iatr.Send(getUsersQuery));
                        await _cache.SetRecordAsync(key, userInbox);               
                }         
            return View(userInbox);
        }       

        [HttpPost]
        [IdProvidedValidation(IdName = "inboxItemId")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<InboxItem>> UpdateInboxItem([FromForm] UpdateInboxItemDto updateInboxItemDto,Guid inboxItemId,DateTime entryDate)
        {       
            var authenticatedId = Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

            await _inboxRepository.UpdateInboxItem(updateInboxItemDto, inboxItemId);
            var catItem = await _catRepository.CreateCat(authenticatedId, inboxItemId,entryDate);
            await _raportRepository.CreateRaport(authenticatedId, catItem, inboxItemId, entryDate);
            return RedirectToAction("Inbox", "Inbox");
        }

        [HttpDelete]
        [IdProvidedValidation(IdName = "inboxItemId")]
        public async Task<ActionResult<bool>> DeleteInboxItem([FromRoute] int inboxItemId)
        {
            bool myInboxItem = await _inboxRepository.DeleteInboxItem(Guid.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value), inboxItemId);

            return Ok(true);
        }
    }
}