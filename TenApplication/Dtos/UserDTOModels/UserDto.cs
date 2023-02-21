
using TenApplication.Models;
using TenApplication.Dtos.InboxDTO;
using TenApplication.Models.RaportModels;

namespace TenApplication.Dtos.DesignerDTOModels
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }       
        public string? CCtr { get; set; }
        public string? ActTyp { get; set; }
        public UserRole UserRole { get; set; }
        public Level Level { get; set; }
        public byte[]? Photo { get; set; }
        public DateTime TennecoStartDate { get; set; }
        public List<InboxItemDto>? InboxItems { get; set; }
        public List<Cat>? Cats { get; set; }
        public List<RaportRecord>? RaportRecords { get; set; }
        public double Experience
        {
            get
            {
                var today = DateTime.Today;
                var months = today.Month - TennecoStartDate.Month;
                double value = months / 12;
                if (TennecoStartDate > today.AddMonths(-months)) ;
                return Math.Round(value, 1);
            }
        }
    }
}
