using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Data.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Update(T item)
        {
            _context.Set<T>().Attach(item);
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var currentItem = _context.Set<T>().Find(id);
            _context.Set<T>().Remove(currentItem);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
