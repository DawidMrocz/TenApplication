namespace TenApplication.Dtos.CatDTOModels
{
    public class CatDto
    {
        public Guid CatId { get; set; }

        //RELATIONS  
        public DateTime CatDate { get; set; }
        public double AllHours { get; set; }
        public List<CatRecordDto> CatRecords { get; set; } = new List<CatRecordDto>();
    }
}
