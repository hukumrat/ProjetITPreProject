using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        List<Employee> ListAllWithIncludes();
        Employee GetByIdWithIncludes(int id);
        List<Employee> ListAllByCompanyIdWithIncludes(int id);
        Employee GetEmployeeByUserId(string? userId);
    }
}
