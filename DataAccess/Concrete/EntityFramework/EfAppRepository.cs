using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAppRepository : IAppRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITempDataDictionaryFactory _tempDataDictionaryFactory;
        public EfAppRepository(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IHttpContextAccessor httpContextAccessor, ITempDataDictionaryFactory tempDataDictionaryFactory)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContextAccessor = httpContextAccessor;
            _tempDataDictionaryFactory = tempDataDictionaryFactory;
        }

        public void IziToast(string message = "Operation completed successfully!", string header = "Success", string type= "success")
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var tempData = _tempDataDictionaryFactory.GetTempData(httpContext);
            tempData["Message"] = message;
            tempData["Header"] = header;
            tempData["Type"] = type;
        }
        public List<SelectListItem> GetRolesForSelectList()
        {
            var rolesList = _roleManager.Roles.ToList();
            List<SelectListItem>? roles = new List<SelectListItem>();
            foreach (var role in rolesList)
            {
                SelectListItem item = new SelectListItem
                {
                    Value = role.Id,
                    Text = role.Name,
                };
                roles.Add(item);
            }
            return roles;
        }

        public List<SelectListItem> GetUsersForSelectList()
        {
            var usersList = _userManager.Users.ToList();
            List<SelectListItem>? users = new List<SelectListItem>();
            foreach (var user in usersList)
            {
                SelectListItem item = new SelectListItem
                {
                    Value = user.Id,
                    Text = user.UserName
                };
                users.Add(item);
            }
            return users;
        }
        public IdentityRole GetRoleById(string id)
        {
            return _roleManager.Roles.FirstOrDefault(r => r.Id == id);
        }
        public ApplicationUser GetUserByUsername(string username)
        {
            return _userManager.Users.FirstOrDefault(u => u.UserName == username);
        }
        public ApplicationUser GetUserById(string id)
        {
            return _userManager.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<ApplicationUser> ListUsers()
        {
            return _userManager.Users.ToList();
        }

        public List<IdentityRole> ListRoles()
        {
            return _roleManager.Roles.ToList();
        }
    }
}
