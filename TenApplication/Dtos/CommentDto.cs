using TenApplication.Models;

namespace TenApplication.Dtos
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public required string Description { get; set; }
        public required int Likes { get; set; }
        public required string UserComment { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
