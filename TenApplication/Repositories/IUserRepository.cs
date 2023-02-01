using System.Reflection;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IUserRepository
    {
        public Task<UserDto> GetUser();
        public Task<List<UserDto>> GetUsers();
        public Task<bool> CreateUser();
        public Task<bool> UpdateUser();
        public Task<bool> DeleteUser();
    }
}