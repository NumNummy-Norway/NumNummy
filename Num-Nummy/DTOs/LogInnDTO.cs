using System.ComponentModel.DataAnnotations;

namespace Num_Nummy.DTOs
{
    public class LogInnDTO
    {

        [Required]
        public string Email { get; set; }


        [Required]
        public string Password { get; set; }
    }
}
