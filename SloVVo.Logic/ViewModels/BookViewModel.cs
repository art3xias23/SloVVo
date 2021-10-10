using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Notification.Wpf;
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
        private readonly NotificationManager _notificationManager;

        private bool _isFieldEnabled;
        private string _editButtonContent;
        private string _borrowButtonContent;

        public string BorrowButtonContent
        {
            get => _borrowButtonContent;
            set { _borrowButtonContent = value; OnPropertyChanged(nameof(BorrowButtonContent)); }
        }

        private Location _selectedLocation;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set { _selectedLocation = value;OnPropertyChanged(nameof(SelectedLocation)); }
        }

        private Section _selectedSection;

        public Section SelectedSection
        {
            get => _selectedSection;
            set { _selectedSection = value;OnPropertyChanged((nameof(SelectedSection))); }
        }

        private Author _selectedAuthor;

        public Author SelectedAuthor
        {
            get => _selectedAuthor;
            set { _selectedAuthor = value;OnPropertyChanged(nameof(SelectedAuthor)); }
        }

        public ICommand LoadWindowCommand { get; set; }
        public ICommand EditRecordCommand { get; set; }
        public ICommand ShowBorrowCommand { get; set; }

        public Book Book
        {
            get => _book;
            set { _book = value; OnPropertyChanged(nameof(Book)); }
        }

        private Book _currentBook;
        public BookViewModel(Book book, IUnitOfWork uow)
        {
            _notificationManager = new NotificationManager();
            _uow = uow;
            _currentBook = new Book();
            _currentBook.LocationId = book.LocationId;
            _currentBook.BookId = book.BookId;
            _currentBook.ShelfId = book.ShelfId;
            _currentBook.BiblioId = book.BiblioId;
            Book = book;

            SelectedAuthor = book.Author;
            SelectedLocation = book.Location;
            SelectedSection = book.Section;

            LoadWindowCommand = new RelayCommandEmpty(LoadWindow);
            EditRecordCommand = new RelayCommandEmpty(EditRecord);
            ShowBorrowCommand = new RelayCommandEmpty(ShowBorrow);
            SetEditButtonContent();
            SetBorrowButtonContent();

            EventAggregator.AuthorUpdateTransmitted += OnAuthorUpdateTransmitted;
            EventAggregator.SectionUpdateTransmitted += OnSectionUpdateTransmitted;
        }

        private void SetBorrowButtonContent()
        {
            BorrowButtonContent = _uow.UserBookRepository.Any(x =>
                x.BiblioId == _currentBook.BiblioId && x.LocationId == _currentBook.LocationId && x.ShelfId == _currentBook.ShelfId &&
                x.BookId == _currentBook.BookId && x.IsTaken) ? "Връщане" : "Наемане";
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
                    x.LocationId == Book.LocationId && x.IsTaken);

                userBook.IsTaken = false;
                userBook.DateOfActualReturning = DateTime.Now;
                _uow.SaveChanges();

                ViewEventHandler.RaiseShowBookEvent(Book);

                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Green,
                        Foreground = Brushes.White,
                        Title = "Книга",
                        Message = "Книга успешно върната",
                        Type = NotificationType.Success
                    }, "WindowArea", TimeSpan.FromSeconds(3));
            }
        }

        private void EditRecord()
        {
            if (EditButtonContent == "Запази")
            {
                DeleteRecord();
                InsertRecord();
                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Green,
                        Foreground = Brushes.White,
                        Title = "Книга",
                        Message = "Книга успешно променена",
                        Type = NotificationType.Success
                    }, "WindowArea", TimeSpan.FromSeconds(3));
            }
            SetEditButtonContent();
        }

        private void DeleteRecord()
        {
            _uow.BookRepository.Delete(x =>
                x.LocationId == _currentBook.LocationId && x.BiblioId == _currentBook.BiblioId && x.ShelfId == _currentBook.ShelfId &&
                x.BookId == _currentBook.BookId);
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
                Location = SelectedLocation,
                BiblioId = Book.BiblioId,
                ShelfId = Book.ShelfId,
                Author = SelectedAuthor,
                Section = SelectedSection,
                BookName = Book.BookName,
                BookId = Book.BookId,
                YearOfPublication = Book.YearOfPublication
            };
            _uow.BookRepository.Add(newBook);
            _uow.SaveChanges();
            _currentBook = newBook;
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
