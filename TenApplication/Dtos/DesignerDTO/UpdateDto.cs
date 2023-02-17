namespace TenApplication.DTO.DesignerDTO
{
    public class UpdateDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}