using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels
{
    public class UsersCreateViewModel
    {
        [Required]
        public string? Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",
            ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        [Required]
        public string? RoleId { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
