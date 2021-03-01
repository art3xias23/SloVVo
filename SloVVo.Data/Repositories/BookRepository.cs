using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;

namespace SloVVo.Data.Repositories
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository(DbContext context) : base(context)
        {
        }
    }
}
