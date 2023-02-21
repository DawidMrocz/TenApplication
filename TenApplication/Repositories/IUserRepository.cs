
using TenApplication.Dtos;
using TenApplication.Dtos.DesignerDTOModels;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<List<UserDto>> GetUsers();
        public Task<UserDto> GetProfile(Guid UserId);

        public Task<bool> LogIn(LoginDto command);
        public Task<bool> LoginOut();
        public Task<bool> ForgotPassword(string UserEmail);
        public Task<bool> ChangePassword(Guid UserId, string oldPassword, string newPassword, string newPasswordRepeat);
        public Task<bool> ChangeRole(Guid UserId, UserRole role);

        public Task<User> CreateUser(RegisterDto command);
        public Task<bool> DeleteUser(Guid UserId);
        public Task<User> UpdateUser(UpdateDto command,Guid userId);
    }
}
