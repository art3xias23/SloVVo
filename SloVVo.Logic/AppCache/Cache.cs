using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;

namespace SloVVo.Logic.AppCache
{
    public class Cache
    {
        public static List<Book> BooksList { get; private set; }
        private static IUnitOfWork _uow;

        public static void LoadBooks()
        {
            _uow = new UnitOfWork();
            BooksList = _uow.BookRepository.GetAllToList();
        }
    }
}
