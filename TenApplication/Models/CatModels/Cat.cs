
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
        public Guid? UserId { get; set; }
        public User? User { get; set; }
        public List<CatRecord>? CatRecords { get; set; }
    }
}