using TenApplication.Dtos.InboxDTO;

namespace TenApplication.Repositories
{
    public interface IInboxRepository
    {
        public Task<InboxDto> GetInbox(Guid userId);
        public Task UpdateInboxItem(UpdateInboxItemDto inboxItem, Guid inboxItemId);
        public Task DeleteInboxItem(Guid inboxItemId);
        public Task SendHours(Guid inboxItemId, Guid userId, DateTime entryDate, double hours);
    }
}
