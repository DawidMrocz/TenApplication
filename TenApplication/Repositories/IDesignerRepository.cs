using TenApplication.DTO;
using TenApplication.DTO.DesignerDTO;
using TenApplication.DTO.InboxDTO;
using TenApplication.Models;

namespace TenApplication.Repositories
{
    public interface IDesignerRepository
    {
        public Task<List<DesignerDto>> GetDesigners();
        public Task<DesignerDto> GetProfile(int DesignerId);

        public Task<bool> LoginDesigner(LoginDto command);
        public Task<bool> ForgotPassword(string DesignerEmail);
        public Task<bool> ChangePassword(int DesignerId, string oldPassword, string newPassword, string newPasswordRepeat);
        public Task<bool> ChangeRole(int DesignerId, UserRole role);


        public Task<Designer> CreateDesigner(RegisterDto command);
        public Task<bool> DeleteDesigner(int DesignerId);
        public Task<Designer> UpdateDesigner(UpdateDto command, int DesignerId);
    }
}
