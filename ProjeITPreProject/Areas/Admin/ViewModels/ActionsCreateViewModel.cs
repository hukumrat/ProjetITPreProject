using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class ActionsCreateViewModel
    {
        

        [Display(Name = "Task Name")]
        public string? Name { get; set; }
        [Display(Name = "Task Contens")]
        public string? Contents { get; set; }
        [Display(Name = "Start Date")]
        public string? StartDate { get; set; }
        [Display(Name = "Finish Date")]
        public string? FinishDate { get; set; }
        [Display(Name = "Is Important")]
        public bool IsImportant { get; set; }
        [Display(Name = "Is Urgent")]
        public bool IsUrgent { get; set; }
        public string? Status { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public List<SelectListItem>? Companies { get; set; }

        [Required]
        [Display(Name = "Task")]
        public int TaskId { get; set; }
        [Display(Name = "Task")]
        public Entities.Concrete.Task? Task { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
