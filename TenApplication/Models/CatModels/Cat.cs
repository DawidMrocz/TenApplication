
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public Guid CatId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CatDate { get; set; }

        //RELATIONS
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}