
using TenApplication.Models;


namespace TenApplication.Dtos.JobDTOModels
{
    public class JobDto
    {
        public int JobId { get; set; }
        public string? JobDescription { get; set; }
        public TaskType? TaskType { get; set; }
        public Software? Software { get; set; }
        public string? Link { get; set; }
        public string? EngineerName { get; set; }
        public List<DesignerDto>? Designers { get; set; }
        public int? Ecm { get; set; }
        public int? Gpdm { get; set; }
        public Region? Region { get; set; }
        public string? ProjectNumber { get; set; }
        public Client? Client { get; set; }
        public string? ProjectName { get; set; }
        public int? Status { get; set; }
        public DateTime? Received { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Finished { get; set; }
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
                    case "NA": return "RECEIVER OF NA";
                    case "CN": return "RECEIVER OF CN";
                    case "IN": return "RECEIVER OF IN";
                    default: return "RECEIVER OF RYB";
                }
            }
        }
    }
}
