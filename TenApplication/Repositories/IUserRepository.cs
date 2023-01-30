using System.Reflection;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUser(GetUserQuery query);
        public Task<List<UserDto>> GetUsers(GetUsersQuery query);
        public Task<bool> CreateUser(CreateUserCommand command);
        public Task<bool> UpdateUser(UpdateUserCommand command);
        public Task<bool> DeleteUser(DeleteUserCommand command);
        public Task<User> AddAttribute(CustomAttributeDto command, int userId);
        public Task<bool> RemoveAttribute(RemoveAttributeCommand command);
        public Task<List<User>> GetUsersForRaport();
        public bool VerifyName(string firstName, string lastName);
    }
}