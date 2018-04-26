using System.ComponentModel.DataAnnotations;

namespace Superstars.WebApp.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [StringLength(64, MinimumLength = 1)]
        public string Pseudo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
