using HandwrittenTextRecognitionSystem.Consts;
using HandwrittenTextRecognitionSystem.Validations;
using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class AddStudentDto
    {
        [MaxLength(30, ErrorMessage = AppErrors.MaxLength)]
        public string FirstName { get; set; } = null!;
        [MaxLength(30, ErrorMessage = AppErrors.MaxLength)]
        public string LastName { get; set; } = null!;
        [RegularExpression(AppRegexs.Username, ErrorMessage = AppErrors.UsernameRegex)]
        [ValidateUniqueUser]
        public string UserName { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; } = null!;
        [RegularExpression(AppRegexs.Password, ErrorMessage = AppErrors.PasswordRegex)]
        public string Password { get; set; } = null!;
        [Compare(nameof(Password), ErrorMessage = AppErrors.ComparePassword)]
        public string ConfirmedPassword { get; set; } = null!;
        [Range(1, 4, ErrorMessage = "Level must be between 1 and 4")]
        public int Level { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public int DepartmentId { get; set; }

    }
}
