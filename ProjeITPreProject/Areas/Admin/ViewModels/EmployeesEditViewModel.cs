using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class EmployeesEditViewModel
    {
        public EmployeesEditViewModel()
        {
            Companies = new List<SelectListItem>();
        }
        public int Id { get; set; }
        public string? UserId { get; set; }
        [Required]
        public int CompanyId { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Surname")]
        public string? Surname { get; set; }
        public List<SelectListItem> Companies { get; set; }
    }
}
