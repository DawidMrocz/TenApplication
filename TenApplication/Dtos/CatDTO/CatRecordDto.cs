namespace TenApplication.DTO.CatRecordDto
{
    public class CatRecordDto
    {
        public int CatRecordId { get; set; }    

        public double CellHours { get; set; }

        public DateTime Created { get; set; }   
   
        //RELATIONS  
        public required Region? Region { get; set; }
        public string? ProjectNumber { get; set; }
        public string? ProjectName { get; set; }      
        public string SapText
        {
            get
            {
                return $"{Region}_{ProjectNumber}_{ProjectName}";
            }
        }
        public string Receiver
        {
            get
            {
                Enum regionValue = Region!;
                switch (regionValue!.ToString())
                {
                    case "NA": return "RYB-18-513-2-005";
                    case "SA": return "RYB-18-514-2-005";
                    case "CN": return "RYB-18-510-2-005";
                    case "IN": return "RYB-18-511-2-005";
                    case "JP": return "RYB-18-512-2-005";
                    default: return "RECEIVER OF RYB";
                }
            }
        }
        
       
    }
}
