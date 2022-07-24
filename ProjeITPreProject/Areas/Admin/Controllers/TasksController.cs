using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;
using System.Globalization;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager,Employee")]
    [Area("Admin")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly ICommentService _commentService;
        private readonly IActionService _actionService;
        private readonly IAppService _appService;
        private readonly UserManager<ApplicationUser> _userManager;
        public TasksController(ITaskService taskService, ICompanyService companyService, IEmployeeService employeeService, IAppService appService, ICommentService commentService, IActionService actionService, UserManager<ApplicationUser> userManager)
        {
            _taskService = taskService;
            _companyService = companyService;
            _employeeService = employeeService;
            _appService = appService;
            _commentService = commentService;
            _actionService = actionService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tasks = _taskService.ListAllWithIncludes();

            if (User.IsInRole("Manager"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                tasks = _taskService.ListAllByUserIdWithIncludes(currentUser.Id);
            }else if (User.IsInRole("Employee"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                var employee = _employeeService.GetEmployeeByUserId(currentUser.Id);
                tasks = _taskService.GetByEmployeeIdWithIncludes(employee.Id);
            }
            return View(tasks);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companies = _companyService.GetCompaniesForSelectList();
            if (User.IsInRole("Manager"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                companies = _companyService.GetCompaniesByUserIdForSelectList(currentUser.Id);
            }
            TasksCreateViewModel model = new TasksCreateViewModel
            {
                Companies = companies
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(TasksCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                DateTime startDate = DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime finishDate = DateTime.ParseExact(model.FinishDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                TimeSpan remainingTime = finishDate.Subtract(startDate);
                Entities.Concrete.Task task = new Entities.Concrete.Task
                {
                    Name = model.Name,
                    CompanyId = model.CompanyId,
                    Contents = model.Contents,
                    StartDate = model.StartDate,
                    FinishDate = model.StartDate,
                    RemainingDays = remainingTime.Days,
                    IsImportant = model.IsImportant,
                    IsUrgent = model.IsUrgent,
                    Status = model.Status
                };
                _taskService.Add(task);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Tasks", new { area = "Admin" });
            }
            model.Companies = _companyService.GetCompaniesForSelectList(); ;
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var task = _taskService.GetByIdWithIncludes(id);
            if (task == null)
            {
                ViewBag.ErrorMessage = $"Task with Id={id} cannot be found";
                _appService.IziToast($"Task with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            var companies = _companyService.GetCompaniesForSelectList();
            if (User.IsInRole("Manager"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                companies = _companyService.GetCompaniesByUserIdForSelectList(currentUser.Id);
            }
            

            TasksEditViewModel model = new TasksEditViewModel
            {
                Id = task.Id,
                Name = task.Name,
                Contents = task.Contents,
                StartDate = task.StartDate,
                FinishDate = task.FinishDate,
                IsImportant = task.IsImportant,
                IsUrgent = task.IsUrgent,
                Status = task.Status,
                CompanyId = task.CompanyId,
                Companies = companies
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(TasksEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var task = _taskService.GetByIdWithIncludes(model.Id);
                if (task == null)
                {
                    ViewBag.ErrorMessage = $"Task with Id={model.Id} cannot be found";
                    _appService.IziToast($"Task with Id={model.Id} cannot be found", "Error", "error");
                    return View("404");
                }
                DateTime startDate = DateTime.ParseExact(model.StartDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime finishDate = DateTime.ParseExact(model.FinishDate, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                TimeSpan remainingTime = finishDate.Subtract(startDate);
                task.Name = model.Name;
                task.Contents = model.Contents;
                task.StartDate = model.StartDate;
                task.FinishDate = model.FinishDate;
                task.RemainingDays = remainingTime.Days;
                task.IsImportant = model.IsImportant;
                task.IsUrgent = model.IsUrgent;
                task.Status = model.Status;
                task.CompanyId = model.CompanyId;
                _taskService.Update(task);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Tasks", new { area = "Admin" });
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                Entities.Concrete.Task task = _taskService.GetById(id);
                if (task == null)
                {
                    ViewBag.ErrorMessage = $"Task with Id={id} cannot be found";
                    _appService.IziToast($"Task with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                List<Comment> comments = _commentService.ListAllByTaskIdWithIncludes(task.Id);
                List<Entities.Concrete.Action> actions = _actionService.ListAllByTaskIdWithIncludes(task.Id);
                foreach (var comment in comments)
                {
                    _commentService.Delete(comment);
                }
                foreach (var action in actions)
                {
                    _actionService.Delete(action);
                }

                _taskService.Delete(task);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Tasks", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Tasks", new { area = "Admin" });
        }

        [HttpPost]
        public JsonResult GetEmployeesByCompanyId(int id)
        {
            var employees = _employeeService.ListAllByCompanyIdWithIncludes(id);
            var employeesForSelect = new Dictionary<int, string?>();
            foreach (var employee in employees)
            {
                employeesForSelect.Add(employee.Id, employee.Name + " " + employee.Surname);
            }
            return Json(employeesForSelect);
        }
    }
}
