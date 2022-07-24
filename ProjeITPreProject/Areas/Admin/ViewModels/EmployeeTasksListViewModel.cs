using Entities.Concrete;

namespace ProjeITPreProjectMvcUI.Areas.Admin.ViewModels
{
    public class EmployeeTasksListViewModel
    {
        public Employee? Employee { get; set; }
        public List<Entities.Concrete.Task>? Tasks { get; set; }
        public int EmployeeId { get; set; }
    }
}
