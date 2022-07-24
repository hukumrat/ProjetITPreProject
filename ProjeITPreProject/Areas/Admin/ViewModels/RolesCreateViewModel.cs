using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels
{
    public class RolesCreateViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string? Name { get; set; }
    }
}
