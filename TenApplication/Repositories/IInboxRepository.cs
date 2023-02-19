using TenApplication.Dtos.InboxDTO;

namespace TenApplication.Repositories
{
    public interface IInboxRepository
    {
        public Task<InboxDto> GetInbox(int userId);
        public Task UpdateInboxItem(UpdateInboxItemDto inboxItem, int id);
        public Task DeleteInboxItem(int id);
        public Task SendHours(int inboxItemId, int userId, DateTime entryDate, double hours);
    }
}
