using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class TasksCreateViewModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Task Name")]
        public string? Name { get; set; }
        [Required]
        [Display(Name = "Task Contens")]
        public string? Contents { get; set; }
        [Required]
        [Display(Name = "Start Date")]
        public string? StartDate { get; set; }
        [Required]
        [Display(Name = "Finish Date")]
        public string? FinishDate { get; set; }
        public DateTime RemainingTime { get; set; }
        [Required]
        [Display(Name = "Is Important")]
        public bool IsImportant { get; set; }
        [Required]
        [Display(Name = "Is Urgent")]
        public bool IsUrgent { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string? Status { get; set; }
        [Required]
        [Display(Name = "Company")]
        public int CompanyId { get; set; }
        public Company? Company{ get; set; }
        public List<SelectListItem>? Companies { get; set; }
        
    }
}
