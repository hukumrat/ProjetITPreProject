using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Employee")]
    [Area("Admin")]
    public class ActionsController : Controller
    {
        private readonly IAppService _appService;
        private readonly IActionService _actionService;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly ITaskService _taskService;
        private readonly UserManager<ApplicationUser> _userManager;
        public ActionsController(IAppService appService, IActionService actionService, ICompanyService companyService, ITaskService taskService, UserManager<ApplicationUser> userManager, IEmployeeService employeeService)
        {
            _appService = appService;
            _actionService = actionService;
            _companyService = companyService;
            _taskService = taskService;
            _userManager = userManager;
            _employeeService = employeeService;
        }

        [HttpGet]
        public IActionResult Index(int id)
        {
            var actions = _actionService.ListAllByTaskIdWithIncludes(id);
            ActionsListViewModel model = new ActionsListViewModel
            {
                Actions = actions,
                TaskId = id
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult Create(int id)
        {
            var task = _taskService.GetByIdWithIncludes(id);
            if (task == null)
            {
                ViewBag.ErrorMessage = $"Task with Id={id} cannot be found";
                _appService.IziToast($"Task with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            ActionsCreateViewModel model = new ActionsCreateViewModel
            {
                TaskId = task.Id,
                Name = task.Name,
                Contents = task.Contents,
                StartDate = task.StartDate,
                FinishDate = task.FinishDate,
                IsImportant = task.IsImportant,
                IsUrgent = task.IsUrgent,
                Status = task.Status,
                CompanyId = task.CompanyId,
                Companies = _companyService.GetCompaniesForSelectList()
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Create(ActionsCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = _taskService.GetByIdWithIncludes(model.TaskId);
                var userId = _userManager.GetUserId(User);
                var employee = _employeeService.GetEmployeeByUserId(userId);
                if (task == null)
                {
                    ViewBag.ErrorMessage = $"Task with Id={model.TaskId} cannot be found";
                    _appService.IziToast($"Task with Id={model.TaskId} cannot be found", "Error", "error");
                    return View("404");
                }
                task.Status = model.Status;
                _taskService.Update(task);
                Entities.Concrete.Action action = new Entities.Concrete.Action
                {
                    TaskId = model.TaskId,
                    Description = model.Description,
                    EmployeeId = employee.Id
                };
                _actionService.Add(action);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Actions", new { area = "Admin", id = model.TaskId });
            }
            model.Companies = _companyService.GetCompaniesForSelectList(); ;
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, int taskId)
        {
            if (ModelState.IsValid)
            {
                var action = _actionService.GetById(id);
                if (action == null)
                {
                    ViewBag.ErrorMessage = $"Action with Id={id} cannot be found";
                    _appService.IziToast($"Action with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                _actionService.Delete(action);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Actions", new { area = "Admin", id = taskId });
            }
            return RedirectToAction("Index", "Actions", new { area = "Admin" , id = taskId });
        }

    }


}
