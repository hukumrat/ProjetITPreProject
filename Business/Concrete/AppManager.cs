using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AppManager : IAppService
    {
        private readonly IAppRepository _appRepository;
        public AppManager(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IdentityRole GetRoleById(string id)
        {
            return _appRepository.GetRoleById(id);
        }

        public List<SelectListItem> GetRolesForSelectList()
        {
            return _appRepository.GetRolesForSelectList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return _appRepository.GetUserById(id);
        }

        public ApplicationUser GetUserByUsername(string username)
        {
            return _appRepository.GetUserByUsername(username);
        }

        public List<SelectListItem> GetUsersForSelectList()
        {
            return _appRepository.GetUsersForSelectList();
        }

        public void IziToast(string message, string header, string type)
        {
            _appRepository.IziToast(message, header, type);
        }

        public List<IdentityRole> ListRoles()
        {
            return _appRepository.ListRoles();
        }

        public List<ApplicationUser> ListUsers()
        {
            return _appRepository.ListUsers();
        }
    }
}
