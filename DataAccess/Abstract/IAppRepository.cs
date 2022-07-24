using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IAppRepository
    {
        void IziToast(string message = "Operation completed successfully!", string header = "Success", string type = "success");
        List<SelectListItem> GetRolesForSelectList();
        IdentityRole GetRoleById(string id);
        ApplicationUser GetUserByUsername(string username);
        ApplicationUser GetUserById(string id);
        List<ApplicationUser> ListUsers();
        List<IdentityRole> ListRoles();
        List<SelectListItem> GetUsersForSelectList();
    }
}
