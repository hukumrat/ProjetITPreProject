using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        void Add(Company company);
        void Delete(Company company);
        void Update(Company company);
        List<Company> ListAll();
        List<Company> ListAllWithIncludes();
        Company GetById(int id);
        Company GetByUserId(string id);
        List<SelectListItem> GetCompaniesForSelectList();
        Company GetByIdWithIncludes(int id);
        List<SelectListItem> GetCompaniesByUserIdForSelectList(string? id);
    }
}
