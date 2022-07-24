using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EfDataContext _context;

        public EfGenericRepository(EfDataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Add(entity);
            SaveAll();
        }

        public void Delete(T entity)
        {
            _context.Remove(entity);
            SaveAll();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public List<T> ListAll()
        {
            return _context.Set<T>().ToList();
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }

        public void Update(T entity)
        {
            _context.Update(entity);
            SaveAll();
        }
    }
}
