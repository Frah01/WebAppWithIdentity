using System.ComponentModel.DataAnnotations;

namespace WebAppWithIdentity.ViewModels
{
    public class RegisterVM
    {
        [Required(ErrorMessage = "Please enter your Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter your email")]
        [DataType(DataType.EmailAddress)]

        public string? Email { get; set; }


        [Required(ErrorMessage = "Please enter your password")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Password and Confirm Password do not match")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


        [Display(Name = "Remember Me")]
        public string? Address { get; set; }

    }
}
