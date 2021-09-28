using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class AddBookViewModel : ObservableObject
    {
        private IUnitOfWork _uow;

        private ObservableCollection<Author> _authors;
        private ObservableCollection<Section> _sections;
        private ObservableCollection<Location> _locations;

        public Book  Book { get; set; }
        

        public ICommand AddContentCommand { get; set; }
        public ICommand AddAuthorCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public ICommand UploadBookCommand { get; set; }
        public AddBookViewModel(IUnitOfWork uow)
        {
            _uow = uow;
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

            EventAggregator.UpdateBookCollection();
            ViewEventHandler.RaiseShowBooksEvent();
        }

        private void AddBookItem()
        {
            var recordExists = _uow.BookRepository.GetAll().Exists(x=>x.LocationId == Book.Location.LocationId && x.BiblioId == Book.BiblioId && x.ShelfId == Book.ShelfId && x.BookId == Book.BookId);
            if (!recordExists)
            {
                _uow.BookRepository.Add(new Book()
                {
                    LocationId = Book.Location.LocationId,
                    BiblioId = Book.BiblioId,
                    ShelfId = Book.ShelfId,
                    BookId = Book.BookId,
                    BookName = Book.BookName,
                    Author = Book.Author,
                    Section = Book.Section,
                    YearOfPublication = Book.YearOfPublication
                });

                _uow.SaveChanges();
                Book=new Book();
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

        public ObservableCollection<Location> Locations 
        {
            get => _locations;
            set { _locations = value;OnPropertyChanged(nameof(Locations)); }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set { _selectedLocation = value;OnPropertyChanged(nameof(SelectedLocation)); }
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
            LoadLocationsCollection();
        }

        private void LoadLocationsCollection()
        {
            Locations = new ObservableCollection<Location>(_uow.LocationRepository.GetAll());
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
