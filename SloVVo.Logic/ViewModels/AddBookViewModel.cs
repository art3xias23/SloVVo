using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using NLog;
using Notification.Wpf;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;
using SloVVo.Logic.Response;

namespace SloVVo.Logic.ViewModels
{
    public class AddBookViewModel : ObservableObject
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private readonly IUnitOfWork _uow;

        private ObservableCollection<Author> _authors;
        private ObservableCollection<Location> _locations;
        private readonly NotificationManager _notificationManager;

        public Book Book { get; set; }


        public ICommand LoadWindowCommand { get; set; }
        public ICommand UploadBookCommand { get; set; }
        public AddBookViewModel(IUnitOfWork uow)
        {
            _notificationManager = new NotificationManager();
            _uow = uow;
            Book = new Book();
            LoadWindowCommand = new RelayCommandEmpty(LoadWindow);
            UploadBookCommand = new RelayCommandEmpty(UploadBook);
        }

        private void UploadBook()
        {
            if (AddBookItem().Success)
            {
                EventAggregator.UpdateBookCollection();
                ViewEventHandler.RaiseShowBooksEvent();
            }
        }

        private IResponse AddBookItem()
        {
            try
            {

                var recordExists = _uow.BookRepository.GetAllToList().Exists(x =>
                    x.LocationId == Book.Location.LocationId && x.BiblioId == Book.BiblioId &&
                    x.ShelfId == Book.ShelfId && x.BookId == Book.BookId);

                if (!recordExists)
                {
                    _logger.Trace("Started Inserting new Book record");
                    _uow.BookRepository.Add(new Book()
                    {
                        LocationId = Book.Location.LocationId,
                        BiblioId = Book.BiblioId,
                        ShelfId = Book.ShelfId,
                        BookId = Book.BookId,
                        BookName = Book.BookName,
                        AuthorName = Book.AuthorName,
                    });

                    _uow.SaveChanges();
                    _logger.Trace("Finished Inserting new Book record");
                    _logger.Trace("Started assigning new book to main book on view");
                    Book = new Book();
                    _logger.Trace("Finished assigning new book to main book on view");

                    _notificationManager.Show(
                        new NotificationContent()
                        {
                            Background = Brushes.Green,
                            Foreground = Brushes.White,
                            Title = "Книга",
                            Message = "Книга успешно добавена",
                            Type = NotificationType.Success
                        }, "WindowArea", TimeSpan.FromSeconds(3));
                    return new Response.Response(true);
                }
                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Green,
                        Foreground = Brushes.White,
                        Title = "Книга",
                        Message = "Книгата вече съществува",
                        Type = NotificationType.Warning
                    }, "WindowArea", TimeSpan.FromSeconds(3));
                return new Response.Response(false);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                _notificationManager.Show(new NotificationContent() { Background = Brushes.Red, Foreground = Brushes.White, Title = "Книга", Message = "Проблем при добавянето на книга", Type = NotificationType.Error }, "WindowArea", TimeSpan.FromSeconds(3));
                return new Response.Response(false);
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

        private void LoadWindow()
        {
            LoadAuthorsCollection();
            LoadLocationsCollection();
        }

        private void LoadLocationsCollection()
        {
            Locations = new ObservableCollection<Location>(_uow.LocationRepository.GetAll());
        }

        private void LoadAuthorsCollection()
        {
            try
            {
                _logger.Trace("Started Getting Authors");
                Authors = new ObservableCollection<Author>(_uow.AuthorRepository.GetAll());
                _logger.Trace("Finished Getting Authors");
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }
    }
}
