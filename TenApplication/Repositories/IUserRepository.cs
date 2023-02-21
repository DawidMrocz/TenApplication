
using TenApplication.Dtos;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> GetProfile(int UserId);

        public Task<bool> LoginUser(LoginDto command);
        public Task<bool> ForgotPassword(string UserEmail);
        public Task<bool> ChangePassword(int UserId, string oldPassword, string newPassword, string newPasswordRepeat);
        public Task<bool> ChangeRole(int UserId, UserRole role);

        public Task<ApplicationUser> CreateUser(RegisterDto command);
        public Task<bool> DeleteUser(int UserId);
        public Task<ApplicationUser> UpdateUser(UpdateDto command,int userId);
    }
}
