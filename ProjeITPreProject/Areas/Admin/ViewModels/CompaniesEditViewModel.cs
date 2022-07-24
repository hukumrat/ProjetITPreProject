using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class CompaniesEditViewModel
    {
        public CompaniesEditViewModel()
        {
            Users = new List<SelectListItem>();
        }
        public int Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string? Name { get; set; }
        public List<SelectListItem> Users { get; set; }
    }
}
