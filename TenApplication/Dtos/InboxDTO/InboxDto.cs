



namespace TenApplication.Dtos.InboxDTO
{
    public class InboxDto
    {
        public int InboxId { get; set; }
        public int TaskQuantity { get; set; }
        public double AllHours { get; set; }
        public required int UserId { get; set; }
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public byte[]? Photo { get; set; }
        public List<InboxItemDto>? InboxItems { get; set; }
    }
}
