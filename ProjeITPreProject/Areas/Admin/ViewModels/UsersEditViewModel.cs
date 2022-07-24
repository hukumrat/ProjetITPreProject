using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels
{
    public class UsersEditViewModel
    {
        public UsersEditViewModel()
        {
            Roles = new List<SelectListItem>();
        }
        [Required]
        public string? Id { get; set; }
        [Required]
        public string? Username { get; set; }

        [Required]
        public string? RoleId { get; set; }
        public List<SelectListItem>? Roles { get; set; }
    }
}
