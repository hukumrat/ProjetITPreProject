using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class ActionsEditViewModel
    {
        [Required]
        public int Id { get; set; }
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
        [Required]
        [Display(Name = "Status")]
        public string? Status { get; set; }
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        [Display(Name = "Employee")]
        public int EmployeeId { get; set; }
        public List<SelectListItem>? Companies { get; set; }
        [Required]
        [Display(Name = "Description")]
        public string? Description { get; set; }
    }
}
