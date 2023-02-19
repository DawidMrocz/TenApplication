using TenApplication.Dtos.InboxDTO;
using TenApplication.Models;
using TenApplication.Models.CatModels;

namespace TenApplication.Dtos.CatDTOModels
{
    public class CatRecordDto
    {
        public int CatRecordId { get; set; }

        //RELATIONS  
        public string? CCtr { get; set; }
        public string? ActTyp { get; set; }
        public Region? Region { get; set; }
        public string? ProjectNumber { get; set; }
        public string? ProjectName { get; set; }
        public List<CatRecordCellDto> CatRecordCells { get; set; } = new List<CatRecordCellDto>();
        public string? SapText 
        {
            get
            {
                return $"{Region}_{ProjectNumber}_{ProjectName}";
            }
            set { }
        }
        public string? Receiver
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
            set { }
        }
        
       
    }
}
