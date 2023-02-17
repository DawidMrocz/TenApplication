﻿using TenApplication.Models;

using System.ComponentModel.DataAnnotations;

namespace TenApplication.DTO.DesignerDTO
{
    public class RegisterDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public string Password { get; set; } = null!;
        public required string CCtr { get; set; }
        public required string ActTyp { get; set; }
        public IFormFile? ProfilePhoto { get; set; }
    }
}
