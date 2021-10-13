using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Data.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T item);
        T AddReturn(T item);
        IEnumerable<T> GetAll();
        IQueryable<T> GetAllQueryable();
        List<T> GetAllToList();
        T GetById(object id);
        T GetById(Expression<Func<T, bool>> predicate);
        void Update(T item);
        void Delete(object id);
        void Delete(Expression<Func<T, bool>> predicate);
        bool Any(Expression<Func<T, bool>> predicate);
    }
}
