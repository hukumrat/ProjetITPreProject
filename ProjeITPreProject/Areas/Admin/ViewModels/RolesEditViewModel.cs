using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels
{
    public class RolesEditViewModel
    {
        public RolesEditViewModel()
        {
            Users = new List<string>();
        }
        public string? Id { get; set; }

        [Required(ErrorMessage = "Role name is required!")]
        public string? Name { get; set; }
        public List<string> Users { get; set; }
    }
}
