using System.Data.Entity;
using SloVVo.Data.Context;

namespace SloVVo.Data.Repositories
{

    public  class UnitOfWork 
    {
        private static DbContext _context;
        private static AuthorRepository _authorRepository;
        private static SectionRepository _sectionRepository;
        private static BookRepository _bookRepository;
        private static UserRepository _userRepository;
        private static LocationRepository _locationRepository;
        private static UserBookRepository _userBookRepository;

        public static AuthorRepository AuthorRepository => _authorRepository ?? (_authorRepository = new AuthorRepository(DbContext));
        public static SectionRepository SectionRepository => _sectionRepository ?? ( _sectionRepository = new SectionRepository(DbContext));
        public static BookRepository BookRepository => _bookRepository ?? (_bookRepository = new BookRepository(DbContext));
        public static UserBookRepository UserBookRepository => _userBookRepository ?? (_userBookRepository = new UserBookRepository(DbContext));
        public static UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(DbContext));

        public static LocationRepository LocationRepository => _locationRepository ?? (_locationRepository = new LocationRepository(DbContext));

        public static DbContext DbContext => _context ?? (_context = new SloVVoDataContext());

        public static void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
