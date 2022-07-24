using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeManager(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Add(Employee employee)
        {
            _employeeRepository.Add(employee);
        }

        public void Delete(Employee employee)
        {
            _employeeRepository.Delete(employee);
        }

        public List<Employee> ListAllByCompanyIdWithIncludes(int id)
        {
            return _employeeRepository.ListAllByCompanyIdWithIncludes(id);
        }

        public Employee GetById(int id)
        {
            return _employeeRepository.GetById(id);
        }

        public Employee GetByIdWithIncludes(int id)
        {
            return _employeeRepository.GetByIdWithIncludes(id);
        }

        public List<Employee> ListAll()
        {
            return _employeeRepository.ListAll();
        }

        public List<Employee> ListAllWithIncludes()
        {
            return _employeeRepository.ListAllWithIncludes();
        }

        public void Update(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public Employee GetEmployeeByUserId(string? userId)
        {
            return _employeeRepository.GetEmployeeByUserId(userId);
        }
    }
}
