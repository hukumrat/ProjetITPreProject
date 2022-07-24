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
    public class EfCommentRepository : EfGenericRepository<Comment>, ICommentRepository
    {
        private readonly EfDataContext _context;
        public EfCommentRepository(EfDataContext context) : base(context)
        {
            _context = context;
        }

        public List<Comment> ListAllByCompanyIdWithIncludes(int id)
        {
            return _context.Comments.Include(c => c.Task).Include(c => c.Company).Where(c => c.CompanyId == id).ToList();
        }

        public List<Comment> ListAllByTaskIdWithIncludes(int id)
        {
            return _context.Comments.Include(c => c.Task).Include(t=>t.Company).Where(c=>c.TaskId == id).ToList();
        }
    }
}
