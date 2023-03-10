using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public enum TaskType
    {
        Drawings,
        Models,
        Both,
    }
    public enum Software
    {
        Catia,
        NX,
    }
    public enum Region
    {
        NA,
        CN,
        IN,
        RYB,
    }
    public enum Client
    {
        Stellantis,
        Ford,
        Toyota,
        GM,
        Daimler,
    }
    public class Job
    {  
        public int JobId { get; set; }
        [StringLength(100,ErrorMessage="Description is to long!")]
        public string? JobDescription { get; set; }
        [EnumDataType(typeof(TaskType))]
        public required TaskType TaskType { get; set; }
        [EnumDataType(typeof(Software))]
        public required Software Software { get; set; }
        [Url(ErrorMessage ="Input must be url string!")]
        public string? Link { get; set; }
        public string? Engineer { get; set; }
        [Required]
        [Range(3000000, 4000000, ErrorMessage = "Ecm number out of range!")]
        public required int Ecm { get; set; }
        [Range(1,100,ErrorMessage = "Task number out of range!")]
        public int Gpdm { get; set; }
        [EnumDataType(typeof(Region))]
        public required Region? Region { get; set; }
        [RegularExpression("/([A-Z]{1}[0-9]{9})+/g")]
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