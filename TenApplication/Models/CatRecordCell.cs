namespace TenApplication.Models
{
    public class CatRecordCell
    {
        public int CatRecordCellId { get; set; }
        [Precision(2)]
        [Range(0, 500, ErrorMessage = "Hours number out of range!")]
        public double CellHours { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Day { get; set; }

        //RELATIONS
        public int CatRecordId { get; set; }
        public CatRecord? CatRecord { get; set; }
    }
}