
using TenApplication.Models;

using System.ComponentModel.DataAnnotations;
using TenApplication.Dtos.InboxDTO;

namespace TenApplication.Dtos.DesignerDTOModels
{
    public class DesignerDto
    {
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
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
        public string DisplayName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
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
