
using System.Data.Entity;
using SloVVo.Data.Models;

namespace SloVVo.Data.Repositories
{
    public class UserBookRepository : GenericRepository<UserBooks>
    {
        public UserBookRepository(DbContext context) : base(context)
        {
        }
    }
}
