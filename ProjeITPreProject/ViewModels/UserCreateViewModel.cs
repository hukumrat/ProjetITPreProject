using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.ViewModels
{
    public class UserCreateViewModel
    {
        [Required]
        [EmailAddress]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Passwords does not match!.")]
        public string? ConfirmPassword { get; set; }
    }
}
