using TenApplication.Models;

namespace TenApplication.Dtos
{
    public enum SortBy
    {
        Name_ASC,
        Name_DSC,
        Price_ASC,
        Price_DSC
    }
    public class QueryParams
    {
        public string? SearchBy { get; set; }
        public SortBy? SortBy { get; set; }
        public Client? Client { get; set; }
        public int? Ecm { get; set; }
        public string? Engineer { get; set; }
        public int? PageNumber { get; set; }
    }
}