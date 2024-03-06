using System.ComponentModel.DataAnnotations;

namespace WebAppWithIdentity.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Please enter your User Name")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Display(Name ="Remember Me")]
        public bool RememberMe { get; set; }

    }
}
