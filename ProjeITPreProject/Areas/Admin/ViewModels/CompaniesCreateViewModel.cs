using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class CompaniesCreateViewModel
    {
        public CompaniesCreateViewModel()
        {
            Users = new List<SelectListItem>();
        }
        public string? Id { get; set; }
        [Required]
        public string? UserId { get; set; }
        [Required]
        [Display(Name = "Company")]
        public string? Name { get; set; }
        public List<SelectListItem> Users { get; set; }

    }
}
