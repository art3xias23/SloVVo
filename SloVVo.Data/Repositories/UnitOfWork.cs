using System.Data.Entity;
using SloVVo.Data.Context;
using SloVVo.Data.Models;

namespace SloVVo.Data.Repositories
{

    public class UnitOfWork : IUnitOfWork
    {
        private  DbContext _context;
        private  AuthorRepository _authorRepository;
        private  SectionRepository _sectionRepository;
        private  BookRepository _bookRepository;
        private  UserRepository _userRepository;
        private  LocationRepository _locationRepository;
        private  UserBookRepository _userBookRepository;

        //public  AuthorRepository AuthorRepository => _authorRepository ?? (_authorRepository = new AuthorRepository(DbContext));
        //public  SectionRepository SectionRepository => _sectionRepository ?? ( _sectionRepository = new SectionRepository(DbContext));
        //public  BookRepository BookRepository => _bookRepository ?? (_bookRepository = new BookRepository(DbContext));
        //public  UserBookRepository UserBookRepository => _userBookRepository ?? (_userBookRepository = new UserBookRepository(DbContext));
        //public  UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));
        //public  LocationRepository LocationRepository => _locationRepository ?? (_locationRepository = new LocationRepository(DbContext));

        public  DbContext DbContext => _context ?? (_context = new SloVVoDataContext());

        IGenericRepository<Author> IUnitOfWork.AuthorRepository => _authorRepository ?? (_authorRepository = new AuthorRepository(DbContext));

        IGenericRepository<Section> IUnitOfWork.SectionRepository => _sectionRepository ?? (_sectionRepository = new SectionRepository(DbContext));

        IGenericRepository<Book> IUnitOfWork.BookRepository => _bookRepository ?? (_bookRepository = new BookRepository(DbContext));

        IGenericRepository<UserBooks> IUnitOfWork.UserBookRepository => _userBookRepository ?? (_userBookRepository = new UserBookRepository(DbContext));

        IGenericRepository<User> IUnitOfWork.UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));

        IGenericRepository<Location> IUnitOfWork.LocationRepository => _locationRepository ?? (_locationRepository = new LocationRepository(DbContext));

        public  void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
