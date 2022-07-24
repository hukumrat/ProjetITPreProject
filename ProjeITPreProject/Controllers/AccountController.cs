using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjeITPreProjectMvcUI.ViewModels;

namespace ProjeITPreProjectMvcUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        [Route("Account/Index")]
        public IActionResult Index()
        {
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("Account/Login")]
        public async Task<IActionResult> Login()
        {
            var userAdminFromDatabase = await _userManager.FindByEmailAsync("admin@admin.com");
            if (userAdminFromDatabase == null)
            {
                IdentityRole adminRole = new IdentityRole
                {
                    Name = "Admin"
                };
                IdentityRole managerRole = new IdentityRole
                {
                    Name = "Manager"
                };
                bool isAdminRoleExist = false;
                bool isManagerRoleExist = false;
                foreach (var role in _roleManager.Roles)
                {
                    if (role.Name == adminRole.Name)
                    {
                        isAdminRoleExist = true;
                    }
                }
                if (!isAdminRoleExist)
                    await _roleManager.CreateAsync(adminRole);
                foreach (var role in _roleManager.Roles)
                {
                    if (role.Name == managerRole.Name)
                    {
                        isManagerRoleExist = true;
                    }
                }
                if (!isManagerRoleExist)
                    await _roleManager.CreateAsync(managerRole);
                var userAdmin = new ApplicationUser
                {
                    UserName = "admin",
                    Email = "admin@admin.com"
                };
                await _userManager.CreateAsync(userAdmin, "Admin1.");
                await _userManager.AddToRoleAsync(userAdmin, adminRole.Name);
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Account/Login")]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                ModelState.AddModelError(string.Empty, "Incorrect username or password!");
            }

            return View(model);
        }

        [Route("Account/SignOut")]
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        public async void createUser(string username, string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = username,
                Email = email
            };
            await _userManager.CreateAsync(user, password);
        }
    }
}
