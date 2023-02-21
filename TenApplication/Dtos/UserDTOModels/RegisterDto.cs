using TenApplication.Models;

using System.ComponentModel.DataAnnotations;

namespace TenApplication.Dtos.DesignerDTOModels
{
    public class RegisterDto
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
