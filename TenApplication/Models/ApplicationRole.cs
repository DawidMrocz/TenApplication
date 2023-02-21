using Microsoft.AspNetCore.Identity;

namespace TenApplication.Models
{
    public enum UserRole
    {
        Designer,
        Leader,
        Admin,
    }
    public class ApplicationRole : IdentityRole<Guid>
    {       
        public UserRole UserRole { get; set; }
    }
}
