using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface ICommentRepository:IGenericRepository<Comment>
    {
        List<Comment> ListAllByTaskIdWithIncludes(int id);
        List<Comment> ListAllByCompanyIdWithIncludes(int id);
    }
}
