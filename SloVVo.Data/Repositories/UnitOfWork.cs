using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SloVVo.Data.Context;

namespace SloVVo.Data.Repositories
{

    public class UnitOfWork 
    {
        private DbContext _context;
        private AuthorRepository _authorRepository;
        private SectionRepository _sectionRepository;
        private BookRepository _bookRepository;
        private UserRepository _userRepository;
        private LocationRepository _locationRepository;
        private UserBookRepository _userBookRepository;

        public UnitOfWork()
        {
           _context = new SloVVoDataContext(); 
        }

        public AuthorRepository AuthorRepository => _authorRepository ?? (_authorRepository = new AuthorRepository(_context));
        public SectionRepository SectionRepository => _sectionRepository ?? ( _sectionRepository = new SectionRepository(_context));
        public BookRepository BookRepository => _bookRepository ?? (_bookRepository = new BookRepository(_context));
        public UserBookRepository UserBookRepository => _userBookRepository ?? (_userBookRepository = new UserBookRepository(_context));
        public UserRepository UserRepository => _userRepository ?? (_userRepository = new UserRepository(_context));

        public LocationRepository LocationRepository => _locationRepository ?? (_locationRepository = new LocationRepository(_context));

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
