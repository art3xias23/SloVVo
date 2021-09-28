using System.Data.Entity;
using SloVVo.Data.Models;

namespace SloVVo.Data.Repositories
{
    public interface IUnitOfWork
    {
        IGenericRepository<Author> AuthorRepository { get; }
        IGenericRepository<Section> SectionRepository { get; }
        IGenericRepository<Book> BookRepository { get; }
        IGenericRepository<UserBooks> UserBookRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Location> LocationRepository { get; }
        DbContext DbContext { get; }
        void SaveChanges();
    }
}