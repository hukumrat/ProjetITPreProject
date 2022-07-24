using DataAccess.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfActionRepository : EfGenericRepository<Entities.Concrete.Action>, IActionRepository
    {
        private readonly EfDataContext _context;
        public EfActionRepository(EfDataContext context) : base(context)
        {
            _context = context;
        }

        public List<Entities.Concrete.Action> ListAllByTaskIdWithIncludes(int id)
        {
            return _context.Actions.Include(a=>a.Task).Where(a=>a.TaskId == id).ToList();
        }
    }
}
