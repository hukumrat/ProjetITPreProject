using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ICommentService
    {
        void Add(Comment comment);
        void Delete(Comment comment);
        void Update(Comment comment);
        List<Comment> ListAll();
        Comment GetById(int id);
        List<Comment> ListAllByTaskIdWithIncludes(int id);
        List<Comment> ListAllByCompanyIdWithIncludes(int id);
    }
}
