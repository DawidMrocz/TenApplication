
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public class Cat
    {
        public int CatId { get; set; }
        public DateTime CatDate { get; set; }

        //RELATIONS
        public int UserId { get; set; }
        public Designer? Designer { get; set; }
        public List<CatRecord> CatRecords { get; set; } = new List<CatRecord>();
    }
}