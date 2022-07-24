using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfTaskRepository : EfGenericRepository<Entities.Concrete.Task>, ITaskRepository
    {
        private readonly EfDataContext _dataContext;
        public EfTaskRepository(EfDataContext context) : base(context)
        {
            _dataContext = context;
        }

        public List<Entities.Concrete.Task> GetByEmployeeIdWithIncludes(int id)
        {
            var assigns = _dataContext.Assigns.Include(a => a.Task).Include(a => a.Employee).Where(a => a.EmployeeId == id).ToList();
            List<Entities.Concrete.Task> tasks = new List<Entities.Concrete.Task>();
            foreach (var assign in assigns)
            {
                Entities.Concrete.Task task = _dataContext.Tasks.Include(t => t.Company).FirstOrDefault(t => t.Id == assign.TaskId);
                tasks.Add(task);
            }
            return tasks;
        }

        public Entities.Concrete.Task GetByIdWithIncludes(int id)
        {
            return _dataContext.Tasks.Include(t => t.Company).FirstOrDefault(t => t.Id == id);
        }

        public List<Entities.Concrete.Task> ListAllByCompanyIdWithIncludes(int id)
        {
            return _dataContext.Tasks.Include(t => t.Company).Where(t => t.CompanyId == id).ToList();
        }

        public List<Entities.Concrete.Task> ListAllByUserIdWithIncludes(string id)
        {
            return _dataContext.Tasks.Include(t => t.Company).ThenInclude(c => c.User).Where(t => t.Company.UserId == id).ToList(); ;
        }

        public List<Entities.Concrete.Task> ListAllWithIncludes()
        {
            return _dataContext.Tasks.Include(t => t.Company).ToList();
        }
    }
}
