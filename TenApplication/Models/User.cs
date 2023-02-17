
using System.ComponentModel.DataAnnotations;

namespace TenApplication.Models
{
    public enum Level
    {
        Associative_Engineer,
        Engineer,
        Senior_Engineer,
    }
    public enum UserRole
    {
        Designer,
        Leader,     
        Admin,
    }
    public enum Client
    {
        Stellantis,
        Ford,
        Toyota,
        GM,
        Daimler,
    }
    public abstract class User
    {
        public int UserId { get; set; }
        [NameValidator(50)]
        public required string Name { get; set; }
        [NameValidator(50)]
        public required string Surname { get; set; }
        [EmailValidator(50)]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Phone(ErrorMessage = "Wprowadz poprawny format numeru telefonu!")]
        public string? Phone { get; set; }
        public string DisplayName
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }      
    }

    public class Designer : User, IValidatableObject
    {
        public string? PasswordHash { get; set; }     
        [Required(ErrorMessage = "Pole ActTyp nie może być puste!")]
        [RegularExpression("/[A-Z]{2}[0-9]{4}/g", ErrorMessage = "Wporowadź poprawny format cctr!")]
        public required string CCtr { get; set; }
        [Required(ErrorMessage = "Pole ActTyp nie może być puste!")]
        [RegularExpression("/[A-Z]{1}[0-9]{4}/g", ErrorMessage = "Wporowadź poprawny format acttyp!")]
        public required string ActTyp { get; set; }
        [Required]
        [EnumDataType(typeof(UserRole))]
        public required UserRole UserRole { get; set; }
        [Required]
        [EnumDataType(typeof(Level))]
        public required Level Level { get; set; }
        public byte[]? Photo { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime TennecoStartDate { get; set; }
        public Inbox Inbox { get; set; } = null!;
        public List<Cat> Cats { get; set; } = new List<Cat>();
        public List<Raport> Raports { get; set; } = new List<Raport>();
        public double Experience
        {
            get
            {
                var today = DateTime.Today;
                var months = today.Month - TennecoStartDate.Month;
                double value = months / 12;
                if (TennecoStartDate > today.AddMonths(-months)) ;
                return Math.Round(value, 1);
            }
        }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TennecoStartDate > DateTime.Now)
            {
                yield return new ValidationResult(
                    $"Twoja data rozpoczęcia pracy nie może być większa niż {DateTime.Now}.",
                    new[] { nameof(TennecoStartDate) });
            }
        }
    }
    public class Engineer:User
    {
        public Client Client { get; set; }
    }  
}
