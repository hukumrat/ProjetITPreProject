using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class UsersProfileViewModel
    {
        [Required(ErrorMessage = "Please enter a username.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Please enter a valid password.")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
    }
}
