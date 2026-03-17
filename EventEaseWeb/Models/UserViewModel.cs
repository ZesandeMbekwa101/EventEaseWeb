using System.ComponentModel.DataAnnotations;

namespace EventEaseWeb.Models
{
    public class UserViewModel
    {
        [Required]
        public string FirstName { get; set; } = "";

        [Required]
        public string LastName { get; set; } = "";

        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; } = "";

        public string Role { get; set; } = "Booking Specialist";
    }
}