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
        public ApplicationRole(string roleName) : base(roleName)
        {
        }

        public UserRole UserRole { get; set; }
    }
}
