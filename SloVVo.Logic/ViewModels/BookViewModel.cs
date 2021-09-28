using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class BookViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;

        private ObservableCollection<Author> _authors;
        private ObservableCollection<Section> _sections;
        private ObservableCollection<Location> _locations;
        private Book _book;
        private ObservableCollection<UserBooks> _userBooks;

        private bool _isFieldEnabled;
        private string _editButtonContent;
        private string _borrowButtonContent;

        public string BorrowButtonContent
        {
            get => _borrowButtonContent;
            set { _borrowButtonContent = value; OnPropertyChanged(nameof(BorrowButtonContent)); }
        }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand EditRecordCommand { get; set; }
        public ICommand ShowBorrowCommand { get; set; }

        public Book Book
        {
            get => _book;
            set { _book = value; OnPropertyChanged(nameof(Book)); }
        }

        public Book CurrentBook { private get; set; }
        public BookViewModel(Book book, IUnitOfWork uow)
        {
            _uow = uow;
            CurrentBook = new Book();
            CurrentBook.LocationId = book.LocationId;
            CurrentBook.BookId = book.BookId;
            CurrentBook.ShelfId = book.ShelfId;
            CurrentBook.BiblioId = book.BiblioId;
            Book = book;
            LoadWindowCommand = new RelayCommandEmpty(LoadWindow);
            EditRecordCommand = new RelayCommandEmpty(EditRecord);
            ShowBorrowCommand = new RelayCommandEmpty(ShowBorrow);
            SetEditButtonContent();
            SetBorrowButtonContent(book);

            EventAggregator.AuthorUpdateTransmitted += OnAuthorUpdateTransmitted;
            EventAggregator.SectionUpdateTransmitted += OnSectionUpdateTransmitted;
        }

        private void SetBorrowButtonContent(Book book)
        {
            BorrowButtonContent = _uow.UserBookRepository.Any(x =>
                x.BiblioId == CurrentBook.BiblioId && x.LocationId == CurrentBook.LocationId && x.ShelfId == CurrentBook.ShelfId &&
                x.BookId == CurrentBook.BookId && x.IsTaken == true) ? "Връщане" : "Наемане";
        }

        private void ShowBorrow()
        {
            if (BorrowButtonContent == "Наемане")
            {

                ViewEventHandler.RaiseShowBorrowEvent(Book);
            }
            else
            {
                var userBook = _uow.UserBookRepository.GetById(x =>
                    x.BiblioId == Book.BiblioId && x.BookId == Book.BookId && x.ShelfId == Book.ShelfId &&
                    x.LocationId == Book.LocationId && x.IsTaken == true);

                userBook.IsTaken = false;
                userBook.DateOfActualReturning = DateTime.Now;
                _uow.SaveChanges();

                ViewEventHandler.RaiseShowBookEvent(Book);
            }
        }

        private void EditRecord()
        {
            if (EditButtonContent == "Запази")
            {
                DeleteRecord();
                InsertRecord();
            }
            SetEditButtonContent();
        }

        private void DeleteRecord()
        {
            _uow.BookRepository.Delete(x =>
                x.LocationId == CurrentBook.LocationId && x.BiblioId == CurrentBook.BiblioId && x.ShelfId == CurrentBook.ShelfId &&
                x.BookId == CurrentBook.BookId);
            _uow.SaveChanges();
        }

        private void SetEditButtonContent()
        {
            if (EditButtonContent == "Промени")
            {
                IsFieldEnabled = true;
                EditButtonContent = "Запази";
            }
            else
            {
                IsFieldEnabled = false;
                EditButtonContent = "Промени";
            }
        }

        private void InsertRecord()
        {
            var newBook = new Book()
            {
                Location = Book.Location,
                BiblioId = Book.BiblioId,
                ShelfId = Book.ShelfId,
                Author = Book.Author,
                Section = Book.Section,
                BookName = Book.BookName,
                BookId = Book.BookId,
                YearOfPublication = Book.YearOfPublication
            };
            _uow.BookRepository.Add(newBook);
            _uow.SaveChanges();
            CurrentBook = newBook;
        }

        public string EditButtonContent
        {
            get => _editButtonContent;
            set { _editButtonContent = value; OnPropertyChanged(nameof(EditButtonContent)); }
        }

        public ObservableCollection<UserBooks> UserBooks
        {
            get => _userBooks;
            set { _userBooks = value; OnPropertyChanged(nameof(UserBooks)); }
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
            set { _sections = value; OnPropertyChanged(nameof(Sections)); }
        }

        public ObservableCollection<Location> Locations
        {
            get => _locations;
            set { _locations = value; OnPropertyChanged(nameof(Locations)); }
        }

        private Location _selectedLocation;
        public Location SelectedLocation
        {
            get => _selectedLocation;
            set { _selectedLocation = value; OnPropertyChanged(nameof(SelectedLocation)); }
        }

        public bool IsFieldEnabled
        {
            get => _isFieldEnabled;
            set { _isFieldEnabled = value; OnPropertyChanged(nameof(IsFieldEnabled)); }
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
            LoadUserBooks();
            DisableFields();
        }

        private void LoadUserBooks()
        {
            UserBooks = new ObservableCollection<UserBooks>(_uow.UserBookRepository.GetAll().Where(x =>
                x.BiblioId == Book.BiblioId && x.LocationId == Book.LocationId && x.ShelfId == Book.ShelfId).ToList());
        }

        private void DisableFields()
        {
            IsFieldEnabled = false;
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
    }
}
