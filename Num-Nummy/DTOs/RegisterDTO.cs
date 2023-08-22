using System.ComponentModel.DataAnnotations;

namespace Num_Nummy.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [MinLength(6)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }


        //one number, 1 upper case and 1 lower case at least
        //* means any character since the
        //noneAlphAmarican is false
        //\\d means one at least is number
        //password between 4-8 characters
        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "create a complex password")]
        public string Password { get; set; }

        [Required]
        [MinLength(6)]
        public string DisplayName { get; set; }
    }
}
