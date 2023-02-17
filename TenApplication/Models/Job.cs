﻿
using Microsoft.AspNetCore.Identity;
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
    public class Job
    {
        public int JobId { get; set; }
        [StringLength(100)]
        public string? JobDescription { get; set; }
        public required TaskType TaskType { get; set; }
        public required Software Software { get; set; }
        [Url(ErrorMessage ="Input must be url string!")]
        public string? Link { get; set; }
        public Guid EngineerId { get; set; }
        public Engineer? Engineer { get; set; }
        [Required]
        [Range(3000000, 4000000, ErrorMessage = "Ecm number out of range!")]
        public required int Ecm { get; set; }
        [Required]
        [Range(1,100,ErrorMessage = "Task number out of range!")]
        public int Gpdm { get; set; }
        public required Region? Region { get; set; }
        public string? ProjectNumber { get; set; }
        public Client? Client { get; set; }
        public string? ProjectName { get; set; }
        [Required]
        [Range(30, 100, ErrorMessage = "Value is incorrect!")]
        public required int Status { get; set; } = 0;
        [DataType(DataType.Date)]
        public DateTime? Received { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DueDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? Started { get; set; }
        [DataType(DataType.Date)]
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
                    case "NA": return "RECEIVER OF NA";
                    case "CN": return "RECEIVER OF CN";
                    case "IN": return "RECEIVER OF IN";
                    default: return "RECEIVER OF RYB";
                }
            }
        }
    }
}