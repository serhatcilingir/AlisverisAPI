using System.ComponentModel.DataAnnotations;

namespace Case.Dtos
{
    public class UserRegistrationDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
