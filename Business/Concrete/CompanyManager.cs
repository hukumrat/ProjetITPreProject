using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyManager(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public void Add(Company company)
        {
            _companyRepository.Add(company);
        }

        public void Delete(Company company)
        {
            _companyRepository.Delete(company);
        }

        public Company GetById(int id)
        {
            return _companyRepository.GetById(id);
        }

        public Company GetByIdWithIncludes(int id)
        {
            return _companyRepository.GetByIdWithIncludes(id);
        }

        public Company GetByUserId(string id)
        {
            return _companyRepository.GetByUserId(id);
        }

        public List<SelectListItem> GetCompaniesByUserIdForSelectList(string? id)
        {
            return _companyRepository.GetCompaniesByUserIdForSelectList(id);
        }

        public List<SelectListItem> GetCompaniesForSelectList()
        {
            return _companyRepository.GetCompaniesForSelectList();
        }

        public List<Company> ListAll()
        {
            return _companyRepository.ListAll();
        }

        public List<Company> ListAllWithIncludes()
        {
            return _companyRepository.ListAllWithIncludes();
        }

        public void Update(Company company)
        {
            _companyRepository.Update(company);
        }
    }
}
