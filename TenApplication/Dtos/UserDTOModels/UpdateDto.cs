namespace TenApplication.Dtos.DesignerDTOModels
{
    public class UpdateDto
    {
        public required string UserName { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}