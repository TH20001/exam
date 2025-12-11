using System.ComponentModel.DataAnnotations;

namespace finalProject.Models
{
    // why make a controller by hand when i can just scaffold it
    public class CreateUserViewModel
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public string Role { get; set; }
    }
}
