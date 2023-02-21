



namespace TenApplication.Dtos.InboxDTO
{
    public class InboxDto
    {
        public Guid InboxId { get; set; }
        public int TaskQuantity { get; set; }
        public double AllHours { get; set; }
        public required Guid UserId { get; set; }
        public required string UserName { get; set; }
        public byte[]? Photo { get; set; }
        public List<InboxItemDto>? InboxItems { get; set; }
    }
}
