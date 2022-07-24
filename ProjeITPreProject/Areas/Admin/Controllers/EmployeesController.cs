using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    [Area("Admin")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ICompanyService _companyService;
        private readonly IAppService _appService;
        private readonly ITaskService _taskService;
        private readonly IAssignService _assignService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public EmployeesController(IEmployeeService employeeService, ICompanyService companyService, IAppService appService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ITaskService taskService, IAssignService assignService)
        {
            _employeeService = employeeService;
            _companyService = companyService;
            _appService = appService;
            _userManager = userManager;
            _roleManager = roleManager;
            _taskService = taskService;
            _assignService = assignService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.ListAllWithIncludes();
            return View(employees);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var companies = _companyService.GetCompaniesForSelectList();

            var currentUser = await _userManager.GetUserAsync(User);
            if (User.IsInRole("Manager"))
            {
                companies = _companyService.GetCompaniesByUserIdForSelectList(currentUser.Id);
            }
            EmployeesCreateViewModel model = new EmployeesCreateViewModel
            {
                Companies = companies
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Create(EmployeesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync("Employee"))
                {
                    IdentityRole identityRole = new IdentityRole
                    {
                        Name = "Employee"
                    };
                    await _roleManager.CreateAsync(identityRole);
                }

                string username = model.Name + "." + model.Surname;
                string password = UppercaseFirstLetter(model.Name) + "." + model.Surname + "1";
                var userToCreate = _appService.GetUserByUsername(username);
                if (userToCreate == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = username
                    };
                    var result = await _userManager.CreateAsync(user, password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, "Employee");
                        userToCreate = user;
                    }
                }
                else
                {
                    _appService.IziToast("Name and surname already taken!", "Error", "error");
                    return View(model);
                }

                Employee employee = new Employee
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    CompanyId = model.CompanyId,
                    User = userToCreate
                };
                _employeeService.Add(employee);
                _appService.IziToast(message: "Employee created successfully!" + "Username: " + username + " Password: " + password, header: "Success", type: "success");
                return RedirectToAction("Index", "Employees", new { area = "Admin" });
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            model.Companies = _companyService.GetCompaniesForSelectList();
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var employee = _employeeService.GetByIdWithIncludes(id);
            if (employee == null)
            {
                ViewBag.ErrorMessage = $"Employee with Id={id} cannot be found";
                _appService.IziToast($"Employee with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            var companies = _companyService.GetCompaniesForSelectList();
            if (User.IsInRole("Manager"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
                companies = _companyService.GetCompaniesByUserIdForSelectList(currentUser.Id);
            }
            EmployeesEditViewModel model = new EmployeesEditViewModel
            {
                Id = employee.Id,
                UserId = employee.UserId,
                CompanyId = employee.CompanyId,
                Name = employee.Name,
                Surname = employee.Surname,
                Companies = companies
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(EmployeesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetByIdWithIncludes(model.Id);

                if (employee == null)
                {
                    ViewBag.ErrorMessage = $"Employee with Id={model.Id} cannot be found";
                    _appService.IziToast($"Employee with Id={model.Id} cannot be found", "Error", "error");
                    return View("404");
                }
                employee.Name = model.Name;
                employee.Surname = model.Surname;
                employee.CompanyId = model.CompanyId;
                _employeeService.Update(employee);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Employees", new { area = "Admin" });
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeService.GetByIdWithIncludes(id);
                if (employee == null)
                {
                    ViewBag.ErrorMessage = $"Employee with Id={id} cannot be found";
                    _appService.IziToast($"Employee with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                var assigns = _assignService.ListAllWithIncludesByEmployeeId(id);
                foreach (var assign in assigns)
                {
                    _assignService.Delete(assign);
                }
                var user = employee.User;
                _employeeService.Delete(employee);
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    _appService.IziToast("", "", "");
                    return RedirectToAction("Index", "Employees", new { area = "Admin" });
                }
                foreach (var error in result.Errors)
                {
                    _appService.IziToast("An error occured!.", "Error", "error");
                    ModelState.AddModelError(String.Empty, error.Description);
                }
            }
            return RedirectToAction("Index", "Employees", new { area = "Admin" });
        }
        [HttpGet]
        public IActionResult TasksList(int id)
        {
            Employee employee = _employeeService.GetByIdWithIncludes(id);
            if (employee == null)
            {
                ViewBag.ErrorMessage = $"Employee with Id={id} cannot be found";
                _appService.IziToast($"Employee with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            List<Entities.Concrete.Task> tasks = _taskService.GetByEmployeeIdWithIncludes(employee.Id);
            EmployeeTasksListViewModel model = new EmployeeTasksListViewModel
            {
                Employee = employee,
                Tasks = tasks,
                EmployeeId = employee.Id
            };
            return View(model);
        }
        [HttpGet]
        public IActionResult TasksCreate(int id)
        {
            EmployeeTasksCreateViewModel model = new EmployeeTasksCreateViewModel();
            Employee employee = _employeeService.GetByIdWithIncludes(id);
            List<Entities.Concrete.Task> employeeTasks = _taskService.GetByEmployeeIdWithIncludes(employee.Id);
            List<Entities.Concrete.Task> tasksList = _taskService.ListAllWithIncludes();
            List<SelectListItem> tasksListForSelectList = new List<SelectListItem>();
            if (employeeTasks.Count == 0)
            {
                foreach (var task in tasksList)
                {
                    if (task.CompanyId == employee.CompanyId)
                    {
                        SelectListItem item = new SelectListItem
                        {
                            Value = task.Id.ToString(),
                            Text = task.Name
                        };
                        tasksListForSelectList.Add(item);
                    }
                }
            }
            else
            {
                foreach (var task in tasksList)
                {
                    if (!employeeTasks.Exists(et => et.Id == task.Id))
                    {
                        if (task.CompanyId == employee.CompanyId)
                        {
                            SelectListItem item = new SelectListItem
                            {
                                Value = task.Id.ToString(),
                                Text = task.Name
                            };
                            tasksListForSelectList.Add(item);
                        }
                    }
                }
            }
            model.Employee = employee;
            model.EmployeeId = employee.Id;
            model.Tasks = tasksListForSelectList;
            return View(model);
        }
        [HttpPost]
        public IActionResult TasksCreate(EmployeeTasksCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Assign assign = new Assign
                {
                    TaskId = model.TaskId,
                    EmployeeId = model.EmployeeId
                };
                _assignService.Add(assign);
                _appService.IziToast("", "", "");
                return RedirectToAction("TasksList", "Employees", new { area = "Admin", id = model.EmployeeId });
            }
            return View();
        }

        [HttpPost]
        public IActionResult TasksDelete(int taskId, int employeeId)
        {
            if (ModelState.IsValid)
            {
                Assign assign = _assignService.GetByTaskIdAndEmployeeId(taskId, employeeId);
                if (assign == null)
                {
                    ViewBag.ErrorMessage = $"Assign cannot be found";
                    _appService.IziToast($"Assign cannot be found", "Error", "error");
                    return View("404");
                }
                _assignService.Delete(assign);
                _appService.IziToast("", "", "");
                return RedirectToAction("TasksList", "Employees", new { area = "Admin", id = employeeId });
            }
            return View();
        }
        string UppercaseFirstLetter(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }
            return char.ToUpper(value[0]) + value.Substring(1);
        }
    }
}
