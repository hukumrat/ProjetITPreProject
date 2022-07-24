using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        void Add(Employee employee);
        void Delete(Employee employee);
        void Update(Employee employee);
        List<Employee> ListAll();
        Employee GetById(int id);
        List<Employee> ListAllWithIncludes();
        Employee GetByIdWithIncludes(int id);
        List<Employee> ListAllByCompanyIdWithIncludes(int id);
        Employee GetEmployeeByUserId(string? userId);
    }
}
