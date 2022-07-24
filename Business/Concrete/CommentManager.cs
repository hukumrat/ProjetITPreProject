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
    public class CommentManager : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        public CommentManager(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public void Add(Comment comment)
        {
            _commentRepository.Add(comment);
        }

        public void Delete(Comment comment)
        {
            _commentRepository.Delete(comment);
        }

        public Comment GetById(int id)
        {
            return _commentRepository.GetById(id);
        }

        public List<Comment> ListAllByTaskIdWithIncludes(int id)
        {
            return _commentRepository.ListAllByTaskIdWithIncludes(id);
        }

        public List<Comment> ListAll()
        {
            return _commentRepository.ListAll();
        }

        public void Update(Comment comment)
        {
            _commentRepository.Update(comment);
        }

        public List<Comment> ListAllByCompanyIdWithIncludes(int id)
        {
            return _commentRepository.ListAllByCompanyIdWithIncludes(id);
        }
    }
}
