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
    public class EfAssignRepository : EfGenericRepository<Assign>, IAssignRepository
    {
        private readonly EfDataContext _context;
        public EfAssignRepository(EfDataContext context) : base(context)
        {
            _context = context;
        }

        public Assign GetByTaskIdAndEmployeeId(int taskId, int employeeId)
        {
            return _context.Assigns.Where(a => a.EmployeeId == employeeId).FirstOrDefault(a => a.TaskId == taskId);
        }

        public List<Assign> ListAllWithIncludes()
        {
            return _context.Assigns.Include(a => a.Task).Include(a => a.Employee).ToList();
        }

        public List<Assign> ListAllWithIncludesByEmployeeId(int id)
        {
            return _context.Assigns.Include(a => a.Task).Include(a => a.Employee).Where(a => a.EmployeeId == id).ToList();
        }
    }
}
