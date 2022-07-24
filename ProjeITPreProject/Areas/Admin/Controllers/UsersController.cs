using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ProjeITPreProjectMvcUI.Areas.Admin.ViewModels;
using ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IAppService _appService;
        private readonly ICompanyService _companyService;
        private readonly IEmployeeService _employeeService;
        private readonly IAssignService _assignService;
        private readonly ICommentService _commentService;
        private readonly ITaskService _taskService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public UsersController(IAppService appService, ICompanyService companyService, IEmployeeService employeeService, IAssignService assignService, ICommentService commentService, ITaskService taskService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _appService = appService;
            _companyService = companyService;
            _employeeService = employeeService;
            _assignService = assignService;
            _commentService = commentService;
            _taskService = taskService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var usersList = _appService.ListUsers();
            List<UsersListViewModel> models = new List<UsersListViewModel>();
            foreach (var user in usersList)
            {
                List<string> roles = new List<string>();
                var rolesList = _appService.ListRoles();
                foreach (var role in rolesList)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        roles.Add(role.Name);
                    }
                }
                UsersListViewModel model = new UsersListViewModel
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Roles = roles
                };
                models.Add(model);
            }
            return View(models);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            UsersCreateViewModel model = new UsersCreateViewModel();
            model.Roles = _appService.GetRolesForSelectList();
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(UsersCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToCreate = _appService.GetUserByUsername(model.Username);
                if (userToCreate == null)
                {
                    var user = new ApplicationUser
                    {
                        UserName = model.Username
                    };
                    var role = _appService.GetRoleById(model.RoleId);
                    var result = await _userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(user, role.Name);
                        _appService.IziToast("", "", "");
                        return RedirectToAction("Index", "Users", new { area = "Admin" });
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    model.Roles = _appService.GetRolesForSelectList();
                    _appService.IziToast("Username already taken!", "Error", "error");
                    return View(model);
                }


            }
            model.Roles = _appService.GetRolesForSelectList();
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = _appService.GetUserById(id);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id={id} cannot be found";
                _appService.IziToast($"TasUserk with Id={id} cannot be found", "Error", "error");
                return View("404");
            }
            IdentityRole? userRole = null;
            var roles = _appService.ListRoles();
            foreach (var role in roles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRole = role;
                    break;
                }
            }
            string userRoleId = "";
            if (userRole != null)
                userRoleId = userRole.Id;
            UsersEditViewModel model = new UsersEditViewModel
            {
                Id = user.Id,
                Username = user.UserName,
                RoleId = userRoleId,
                Roles = _appService.GetRolesForSelectList()
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(UsersEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userToUpdate = await _userManager.FindByIdAsync(model.Id);
                if (userToUpdate == null)
                {
                    return View("404");
                }
                var currentUser = await _userManager.GetUserAsync(User);
                IdentityRole? oldRole = null;
                var roles = _appService.ListRoles();
                foreach (var role in roles)
                {
                    if (await _userManager.IsInRoleAsync(userToUpdate, role.Name))
                    {
                        oldRole = role;
                        break;
                    }
                }
                if (oldRole == null)
                {
                    var newRole = _appService.GetRoleById(model.RoleId);
                    await _userManager.AddToRoleAsync(userToUpdate, newRole.Name);
                }
                else if (oldRole.Id != model.RoleId)
                {
                    var newRole = _appService.GetRoleById(model.RoleId);
                    await _userManager.RemoveFromRoleAsync(userToUpdate, oldRole.Name);
                    await _userManager.AddToRoleAsync(userToUpdate, newRole.Name);
                }

                if (userToUpdate == currentUser)
                {
                    userToUpdate.UserName = model.Username;
                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(userToUpdate, false);
                        _appService.IziToast("", "", "");
                        return RedirectToAction("Index", "Users", new { area = "Admin" });
                    }

                    foreach (var error in result.Errors)
                    {
                        _appService.IziToast("An error occured!.", "Error", "error");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
                else
                {
                    userToUpdate.UserName = model.Username;
                    var result = await _userManager.UpdateAsync(userToUpdate);
                    if (result.Succeeded)
                    {
                        _appService.IziToast("", "", "");
                        return RedirectToAction("Index", "Users", new { area = "Admin" });
                    }

                    foreach (var error in result.Errors)
                    {
                        _appService.IziToast("An error occured!.", "Error", "error");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                }
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = _appService.GetUserById(id);
                var company = _companyService.GetByUserId(id);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"Company with Id={id} cannot be found";
                    _appService.IziToast($"Company with Id={id} cannot be found", "Error", "error");
                    return View("404");
                }
                if (company == null)
                {
                    var result = await _userManager.DeleteAsync(user);
                }
                else
                {
                    var employees = _employeeService.ListAllByCompanyIdWithIncludes(company.Id);
                    foreach (var employee in employees)
                    {
                        var assigns = _assignService.ListAllWithIncludesByEmployeeId(employee.Id);
                        foreach (var assign in assigns)
                        {
                            _assignService.Delete(assign);
                        }
                        _employeeService.Delete(employee);
                        await _userManager.DeleteAsync(employee.User);
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
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        _appService.IziToast("", "", "");
                        return RedirectToAction("Index", "Users", new { area = "Admin" });
                    }
                    foreach (var error in result.Errors)
                    {
                        _appService.IziToast("An error occured!.", "Error", "error");
                        ModelState.AddModelError(String.Empty, error.Description);
                    }
                    return RedirectToAction("Index", "Users", new { area = "Admin" });
                }

            }
            _appService.IziToast("", "", "");
            return RedirectToAction("Index", "Users", new { area = "Admin" });
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Profile()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            UsersProfileViewModel model = new UsersProfileViewModel
            {
                Username = currentUser.UserName
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Manager,Employee")]
        public async Task<IActionResult> Profile(UsersProfileViewModel model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return View("404");
            }
            if (ModelState.IsValid)
            {
                if (await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
                {
                    user.UserName = model.Username;
                    var resultUser = await _userManager.UpdateAsync(user);
                    if (!resultUser.Succeeded)
                    {
                        foreach (var error in resultUser.Errors)
                        {
                            ModelState.AddModelError(String.Empty, error.Description);
                            _appService.IziToast("An error occured!", "Error", "error");
                        }
                    }
                    if (!string.IsNullOrEmpty(model.NewPassword) && !string.IsNullOrEmpty(model.ConfirmPassword))
                    {
                        var resultPassword =
                            await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                        if (!resultPassword.Succeeded)
                        {
                            foreach (var error in resultPassword.Errors)
                            {
                                ModelState.AddModelError(String.Empty, error.Description);
                                _appService.IziToast("An error occured.", "Hata", "error");
                            }
                        }
                    }
                    await _signInManager.SignInAsync(user, false);
                    _appService.IziToast("Password has been updated successfully!","Success","success");
                    return RedirectToAction("Profile", "Users", new { area = "Admin" });
                }
                else
                {
                    _appService.IziToast("Current password is wrong!", "Error", "error");
                    return View(model);
                }
            }
            _appService.IziToast("Please fill all required areas.", "Warning", "warning");
            return View(model);
        }

    }
}
