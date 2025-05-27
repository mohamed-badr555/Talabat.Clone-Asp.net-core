using System.ComponentModel.DataAnnotations;

namespace Talabat.API.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*\W).{6,}$",
            ErrorMessage = "Password must have 1 UpperCase,1 LowerCase, 1 number, 1 non alphanumeric and at least 6 characters")]
        public string Password { get; set; }

    }
}
