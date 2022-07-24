using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class EmployeeTasksCreateViewModel
    {
        [Required]
        public int EmployeeId { get; set; }
        [Required]
        public int TaskId { get; set; }
        public List<SelectListItem>? Tasks { get; set; }
        public Employee? Employee{ get; set; }
    }
}
