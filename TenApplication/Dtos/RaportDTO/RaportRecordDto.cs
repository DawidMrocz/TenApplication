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
        
        public int? InboxId { get; set; }
        public Inbox? Inbox { get; set; }
        
        public required Software Software { get; set; }
        public string? Link { get; set; }
        public Guid EngineerId { get; set; }
        public Engineer? Engineer { get; set; }
        [Required]
        [Range(3000000, 4000000, ErrorMessage = "Ecm number out of range!")]
        public required int Ecm { get; set; }
        [Range(1,100,ErrorMessage = "Task number out of range!")]
        public int Gpdm { get; set; }
        [EnumDataType(typeof(Region))]
        public required Region? Region { get; set; }
        [RegularExpression("/([A-Z]{1}[0-9]{9})\w+/g")]
        public string? ProjectNumber { get; set; }
        [EnumDataType(typeof(Client))]
        public Client? Client { get; set; }      
        [RegularExpression("/^[A-Z]{2,3}_E[0-9]{9}_/g")]
        public string? ProjectName { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "Value is incorrect!")]
        public int Status { get; set; } = 0;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Received { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DueDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Started { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Finished { get; set; }

        //RELATIONS
        public List<InboxItem> InboxItems { get; set; } = new List<InboxItem>();
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