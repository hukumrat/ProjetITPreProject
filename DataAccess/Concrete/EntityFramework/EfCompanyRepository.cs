using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCompanyRepository : EfGenericRepository<Company>, ICompanyRepository
    {
        private readonly EfDataContext _dataContext;
        public EfCompanyRepository(EfDataContext context) : base(context)
        {
            _dataContext = context;
        }

        public Company GetByIdWithIncludes(int id)
        {
            return _dataContext.Companies.Include(c => c.User).FirstOrDefault(c=>c.Id == id);
        }

        public Company GetByUserId(string id)
        {
            return _dataContext.Companies.Include(c => c.User).FirstOrDefault(c=>c.UserId == id);
        }

        public List<SelectListItem> GetCompaniesByUserIdForSelectList(string? id)
        {
            var companiesList = _dataContext.Companies.Include(c => c.User).Where(c=>c.UserId == id).ToList();
            List<SelectListItem>? companies = new List<SelectListItem>();
            foreach (var company in companiesList)
            {
                SelectListItem item = new SelectListItem
                {
                    Value = company.Id.ToString(),
                    Text = company.Name,
                };
                companies.Add(item);
            }
            return companies;
        }

        public List<SelectListItem> GetCompaniesForSelectList()
        {
            var companiesList = _dataContext.Companies.Include(c=>c.User).ToList();
            List<SelectListItem>? companies = new List<SelectListItem>();
            foreach (var company in companiesList)
            {
                SelectListItem item = new SelectListItem
                {
                    Value = company.Id.ToString(),
                    Text = company.Name,
                };
                companies.Add(item);
            }
            return companies;
        }

        public List<Company> ListAllWithIncludes()
        {
            return _dataContext.Companies.Include(c => c.User).ToList();
        }

    }
}
