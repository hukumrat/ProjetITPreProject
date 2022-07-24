using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeRepository : EfGenericRepository<Employee>, IEmployeeRepository
    {
        private readonly EfDataContext _dataContext;
        public EfEmployeeRepository(EfDataContext context) : base(context)
        {
            _dataContext = context;
        }

        public List<Employee> ListAllByCompanyIdWithIncludes(int id)
        {
            return _dataContext.Employees.Include(e => e.User).Include(e => e.Company).Where(e => e.CompanyId == id).ToList();
        }

        public Employee GetByIdWithIncludes(int id)
        {
            return _dataContext.Employees.Include(e => e.User).Include(e => e.Company).FirstOrDefault(e=>e.Id == id);
        }

        public List<Employee> ListAllWithIncludes()
        {
            return _dataContext.Employees.Include(e => e.User).Include(e => e.Company).ToList();
        }

        public Employee GetEmployeeByUserId(string? userId)
        {
            return _dataContext.Employees.FirstOrDefault(e=>e.UserId == userId);
        }
    }
}
