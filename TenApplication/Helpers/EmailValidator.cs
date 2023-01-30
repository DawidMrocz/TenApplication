using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using TenApplication.Data;

namespace TenApplication.Helpers
{
    public class EmailValidator : ValidationAttribute
    {
        public int LenghtOfEmail { get; }
        public EmailValidator(int lenghtOfEmail) => LenghtOfEmail = lenghtOfEmail;

        public string GetErrorMessage(string inputValue) => $"E-mail: {inputValue} jest już zajęty!";

        public string GetErrorMessage2() => $"Pole E-mail nie może być puste!";

        public string GetErrorMessage3() => $"Wprowadź poprawny format pola e-mail!";

        public string GetErrorMessage4() => $"Wprowadzona wartość jest za długa!";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            string inputValue = value.ToString();

            if(string.IsNullOrEmpty(inputValue)) new ValidationResult(GetErrorMessage2());

            if (inputValue.Length > LenghtOfEmail || inputValue.Length < 2) return new ValidationResult(GetErrorMessage4());       

            Match m = Regex.Match(inputValue, @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])}");

            if (m.Success is false) return new ValidationResult(GetErrorMessage3());

            ApplicationDbContext context = (ApplicationDbContext)validationContext.GetService(typeof(ApplicationDbContext));

            if (context.Users.Any(e => e.Equals(value))) return new ValidationResult(GetErrorMessage(inputValue));

            return ValidationResult.Success;
        }
    }
}
