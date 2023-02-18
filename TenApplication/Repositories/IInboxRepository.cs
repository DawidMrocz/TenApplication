using TenApplication.Dtos.InboxDTO;

namespace TenApplication.Repositories
{
    public interface IInboxRepository
    {
        public Task<InboxDto> GetInbox(int userId);

        public Task CreateInboxItem(int id,int userId);
        public Task UpdateInboxItem(UpdateInboxItemDto inboxItem, int id);
        public Task DeleteInboxItem(int id);
    }
}
