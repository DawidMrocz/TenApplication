using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace TenApplication.Helpers
{
    public class NameValidator : ValidationAttribute
    {
        public int LenghtOfWord { get; }
        public NameValidator(int lenghtOfWord) => LenghtOfWord = lenghtOfWord;

        public string GetErrorMessage() =>
            $"Fraza musi mieć przynajmniej 2 znaki i byc dłuższa niż {LenghtOfWord} znaków.";

        public string GetErrorMessage2(string input) =>
            $"Imię powinno składać się z samych liter oraz zaczynać się od dużej litery: {input} .";

        public string GetErrorMessage3() =>
            $"Musisz wpisać watość - pole nie może być puste!";

        protected override ValidationResult? IsValid(
            object? value, ValidationContext validationContext)
        {
            //var user = (User)validationContext.ObjectInstance;
            string inputValue = value.ToString();

            if (inputValue is null) return new ValidationResult(GetErrorMessage3());

            if (inputValue.Length > LenghtOfWord || inputValue.Length < 2) return new ValidationResult(GetErrorMessage());

            Match m = Regex.Match(inputValue, @"[A-Z]{1,}[a-z]{1,}");

            if (m.Success is false) return new ValidationResult(GetErrorMessage2(inputValue));

            return ValidationResult.Success;
        }
    }
}
