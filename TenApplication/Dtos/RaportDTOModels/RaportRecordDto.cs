using TenApplication.Models;


namespace TenApplication.Dtos.RaportDTOModels
{
    public class RaportRecordDto
    {
        public Guid RaportRecordId { get; set; }
        public DateTime RaportCreateDate { get; set; }
        public double RaportRecordHours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }

        //RELATIONS
        public string? UserName { get; set; } 
        
        public Software Software { get; set; }
        public int Ecm { get; set; }
        public int Gpdm { get; set; }
        public string? ProjectNumber { get; set; }
        public Client? Client { get; set; }             
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }  

        public double UserRecordHours { get; set; }  
    }
}