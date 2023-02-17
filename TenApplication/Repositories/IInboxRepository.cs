using TenApplication.DTO.InboxDTO;
using TenApplication.Models;
using TenApplicationt.DTO.InboxDTO;

namespace TenApplication.Repositories
{
    public interface IInboxRepository
    {
        public Task<InboxDto> GetInbox(int userId);

        public Task CreateInboxItem(int jobId, int inboxId);
        public Task UpdateInboxItem(UpdateInboxItemDto inboxItem, int inboxItemId);
        public Task DeleteInboxItem(int inboxItem);
    }
}
