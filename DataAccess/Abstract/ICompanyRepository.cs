using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICompanyRepository:IGenericRepository<Company>
    {
        List<Company> ListAllWithIncludes();
        Company GetByUserId(string id);
        Company GetByIdWithIncludes(int id);
        List<SelectListItem> GetCompaniesForSelectList();
        List<SelectListItem>  GetCompaniesByUserIdForSelectList(string? id);
    }
}
