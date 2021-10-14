using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SloVVo.Data.Repositories
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<IEnumerable<T>> GetAllEnumerableAsync();
    }
}
