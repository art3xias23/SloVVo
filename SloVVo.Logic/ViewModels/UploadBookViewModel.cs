using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class UploadBookViewModel : ObservableObject
    {
        private UnitOfWork _uow;

        private ObservableCollection<Author> _authors;
        private ObservableCollection<Section> _sections;

        public Book  Book { get; set; }
        

        public ICommand AddContentCommand { get; set; }
        public ICommand AddAuthorCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand UploadBookCommand { get; set; }
        public UploadBookViewModel()
        {
            _uow = new UnitOfWork();

            Book = new Book();
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
            AddContentCommand = new RelayCommandEmpty(AddContent);
            LoadWindowCommand = new RelayCommandEmpty(LoadWindow);
            UploadBookCommand = new RelayCommandEmpty(UploadBook);

            EventAggregator.AuthorUpdateTransmitted += OnAuthorUpdateTransmitted;
            EventAggregator.SectionUpdateTransmitted += OnSectionUpdateTransmitted;
        }

        private void UploadBook()
        {
            AddBookItem();

            ViewEventHandler.RaiseCloseUploadView();

            EventAggregator.UpdateBookCollection();
        }

        private void AddBookItem()
        {
            var recordExists = _uow.BookRepository.GetAll().Exists(x=>x.BibioId == Book.BibioId && x.ShelfId == Book.ShelfId && x.BookId == Book.BookId);
            if (!recordExists)
            {
                _uow.BookRepository.Add(new Book()
                {
                    BibioId = Book.BibioId,
                    ShelfId = Book.ShelfId,
                    BookId = Book.BookId,
                    BookName = Book.BookName,
                    AuthorId = Book.AuthorId,
                    SectionId = Book.SectionId,
                    YearOfPublication = Book.YearOfPublication
                });

                _uow.SaveChanges();
            }
        }

        public ObservableCollection<Author> Authors
        {
            get => _authors;
            set
            {
                _authors = value;
                OnPropertyChanged(nameof(Authors));
            }
        }

        public ObservableCollection<Section> Sections
        {
            get => _sections;
            set { _sections = value; OnPropertyChanged(nameof(Sections));}
        }

        private void OnSectionUpdateTransmitted()
        {
            LoadSectionsCollection();
        }

        private void OnAuthorUpdateTransmitted()
        {
            LoadAuthorsCollection();
        }
        private void LoadWindow()
        {
            LoadAuthorsCollection();
            LoadSectionsCollection();
        }

        private void LoadSectionsCollection()
        {
            Sections = new ObservableCollection<Section>(_uow.SectionRepository.GetAll());
        }

        private void LoadAuthorsCollection()
        {
            Authors = new ObservableCollection<Author>(_uow.AuthorRepository.GetAll());
        }

        private void AddContent()
        {
            Event.ViewEventHandler.RaiseShowAddContentView();
        }

        private void AddAuthor()
        {
            Event.ViewEventHandler.RaiseShowAddAuthorView();
        }
    }
}
