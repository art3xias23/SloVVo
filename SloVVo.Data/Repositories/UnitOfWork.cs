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

        public UnitOfWork()
        {
           _context = new SloVVoDataContext(); 
        }

        public AuthorRepository AuthorRepository => _authorRepository ?? new AuthorRepository(_context);
        public SectionRepository SectionRepository => _sectionRepository ?? new SectionRepository(_context);
        public BookRepository BookRepository => _bookRepository ?? new BookRepository(_context);
    }
}
