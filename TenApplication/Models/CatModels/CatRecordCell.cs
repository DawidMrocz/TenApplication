using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models.CatModels
{
    public class CatRecordCell
    {
        public Guid CatRecordCellId { get; set; }

        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double CellHours { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CatRecordCellDate { get; set; }

        //RELATIONS
        public Guid CatRecordId { get; set; }
        public CatRecord CatRecord { get; set; } = null!;
    }
}
