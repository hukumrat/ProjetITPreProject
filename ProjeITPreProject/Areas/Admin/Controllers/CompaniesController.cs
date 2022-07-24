using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CompaniesController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly IAssignService _assignService;
        private readonly ICommentService _commentService;
        private readonly ITaskService _taskService;
        private readonly IAppService _appService;
        private readonly UserManager<ApplicationUser> _userManager;
        public CompaniesController(ICompanyService companyService, IEmployeeService employeeService, IAppService appService, UserManager<ApplicationUser> userManager, IAssignService assignService, ICommentService commentService, ITaskService taskService)
        {
            _taskService = taskService;
            _commentService = commentService;
            _companyService = companyService;
            _employeeService = employeeService;
            _appService = appService;
            _userManager = userManager;
            _assignService = assignService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var companies = _companyService.ListAllWithIncludes();
            return View(companies);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var usersList = _userManager.Users.ToList();
            var users = new List<SelectListItem>();
            foreach (var user in usersList)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = user.Id,
                        Text = user.UserName
                    };
                    users.Add(item);
                }
            }
            CompaniesCreateViewModel model = new CompaniesCreateViewModel
            {
                Users = users
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(CompaniesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                Company company = new Company
                {
                    Name = model.Name,
                    UserId = model.UserId
                };
                _companyService.Add(company);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Companies", new { area = "Admin" });
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var company = _companyService.GetById(id);
            if (company == null)
            {
                ViewBag.ErrorMessage = $"Company with Id={id} cannot be found";
                _appService.IziToast($"Company with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            var usersList = _userManager.Users.ToList();
            var users = new List<SelectListItem>();
            foreach (var user in usersList)
            {
                if (await _userManager.IsInRoleAsync(user, "Manager"))
                {
                    SelectListItem item = new SelectListItem
                    {
                        Value = user.Id,
                        Text = user.UserName
                    };
                    users.Add(item);
                }
            }
            if (User.IsInRole("Manager"))
            {
                var currentUser = await _userManager.GetUserAsync(User);
            }
            CompaniesEditViewModel model = new CompaniesEditViewModel
            {
                Id = company.Id,
                UserId = company.UserId,
                Name = company.Name,
                Users = users
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(CompaniesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var company = _companyService.GetById(model.Id);

                if (company == null)
                {
                    ViewBag.ErrorMessage = $"Company with Id={model.Id} cannot be found";
                    _appService.IziToast($"Company with Id={model.Id} cannot be found", "Error", "error");
                    return View("404");
                }
                company.Name = model.Name;
                company.UserId = model.UserId;
                _companyService.Update(company);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Companies", new { area = "Admin" });
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                var company = _companyService.GetById(id);
                if (company == null)
                {
                    ViewBag.ErrorMessage = $"Company with Id={id} cannot be found";
                    _appService.IziToast($"Company with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                var employees = _employeeService.ListAllByCompanyIdWithIncludes(company.Id);
                foreach (var employee in employees)
                {
                    var assigns = _assignService.ListAllWithIncludesByEmployeeId(employee.Id);
                    foreach (var assign in assigns)
                    {
                        _assignService.Delete(assign);
                    }
                    _employeeService.Delete(employee);
                    var result = await _userManager.DeleteAsync(employee.User);
                }
                List<Comment> comments = _commentService.ListAllByCompanyIdWithIncludes(company.Id);
                foreach (var comment in comments)
                {
                    _commentService.Delete(comment);
                }
                
                List<Entities.Concrete.Task> tasks = _taskService.ListAllByCompanyIdWithIncludes(company.Id);
                foreach (var task in tasks)
                {
                    _taskService.Delete(task);
                }
                _companyService.Delete(company);
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Companies", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Companies", new { area = "Admin" });
        }
    }
}
