using System.ComponentModel.DataAnnotations;

namespace Auth.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
