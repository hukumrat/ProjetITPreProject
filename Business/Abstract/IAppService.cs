using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAppService
    {
        void IziToast(string message, string header, string type);
        List<SelectListItem> GetRolesForSelectList();
        IdentityRole GetRoleById(string id);
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserById(string id);
        List<ApplicationUser> ListUsers();
        List<IdentityRole> ListRoles();
        List<SelectListItem> GetUsersForSelectList();

    }
}
