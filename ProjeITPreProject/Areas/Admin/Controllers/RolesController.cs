using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using ProjeITPreProjectMvcUI.Areas.Admin.Views.ViewModels;

namespace ProjeITPreProjectMvcUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly IAppService _appService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public RolesController(IAppService appService, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _appService = appService;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var roles = _appService.ListRoles();
            return View(roles);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(RolesCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.Name
                };
                IdentityResult result = await _roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    _appService.IziToast("","","");
                    return RedirectToAction("Index", "Roles", new { area = "Admin" });
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                IdentityRole role = _appService.GetRoleById(id);
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    _appService.IziToast("", "", "");
                    return RedirectToAction("Index", "Roles", new { area = "Admin" });
                }
                foreach (var error in result.Errors)
                {
                    _appService.IziToast("An error occured!.", "Error", "error");
                    ModelState.AddModelError(String.Empty, error.Description);
                }
                _appService.IziToast("", "", "");
                return RedirectToAction("Index", "Roles", new { area = "Admin" });
            }
            return RedirectToAction("Index", "Roles", new { area = "Admin" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id={id} cannot be found";
                _appService.IziToast($"Role with Id={id} cannot be found", "Error", "error");
                return View("404");
            }

            var model = new RolesEditViewModel
            {
                Id = role.Id,
                Name = role.Name
            };

            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                var isInRole = await _userManager.IsInRoleAsync(user, role.Name);
                if (isInRole)
                {
                    model.Users.Add(user.UserName);
                    
                }
            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(RolesEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = await _roleManager.FindByIdAsync(model.Id);

                if (role == null)
                {
                    ViewBag.ErrorMessage = $"Role with Id={model.Id} cannot be found";
                    _appService.IziToast($"Role with Id={model.Id} cannot be found", "Error", "error");
                    return View("404");
                }
                role.Name = model.Name;
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    _appService.IziToast("", "", "");
                    return RedirectToAction("Index", "Roles", new { area = "Admin" });
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            _appService.IziToast("Please fill all required areas!", "Warning", "warning");
            return View(model);
        }
    }
}
