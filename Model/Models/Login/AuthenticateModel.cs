using System.ComponentModel.DataAnnotations;

namespace Model.Models.Login
{
    public class AuthenticateModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
