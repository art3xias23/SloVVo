using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Data.Repositories
{
   

    public abstract class GenericRepository<T> : IGenericRepository<T>  where T : class
    {
        private DbContext _context;

        protected GenericRepository(DbContext context)
        {
            _context = context;
        }
        public void Add(T item)
        {
            _context.Set<T>().Add(item);
        }
        public T AddReturn(T item)
        {
            return _context.Set<T>().Add(item);
        }

        public IEnumerable<T> GetAllEnumerable()
        {
            return _context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllEnumerableAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public List<T> GetAllToList()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(object id)
        {
            return _context.Set<T>().Find(id);
        }

        public T GetById(Expression<Func<T, bool>> predicate)
        {
            var something = _context.Set<T>().Where(predicate).ToList();
            return something.SingleOrDefault();
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
        public void Delete(Expression<Func<T, bool>> predicate)
        {
            var currentItem = _context.Set<T>().SingleOrDefault(predicate);
            _context.Set<T>().Remove(currentItem);
        }

        public bool Any(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Any(predicate);
        }
    }
 }
