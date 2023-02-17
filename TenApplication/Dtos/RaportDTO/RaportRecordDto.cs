using TenApplication.Models;


namespace TenApplication.DTO.RaportDTO
{
    public class RaportRecordDto
    {
        public int RaportRecordId { get; set; }
        public double RaportRecordHours { get; set; }
        public int Components { get; set; }
        public int DrawingsComponents { get; set; }
        public int DrawingsAssembly { get; set; }

        //RELATIONS
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DisplayName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }      
        
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