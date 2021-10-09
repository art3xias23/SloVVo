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
    public class BorrowViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;

        private Book _book;
        private User _selectedUser;
        private DateTime _selectedDateOfReturning;
        private ObservableCollection<User> _userList;
        private readonly NotificationManager _notificationManager;

        private bool _isFieldEnabled;

        public ICommand BorrowCommand { get; set; }
        public BorrowViewModel(Book book, IUnitOfWork uow)
        {
            _notificationManager = new NotificationManager();
            _uow = uow;
            Book = book;
            DisableFields();
            UserList = GetUsers();
            SelectedDateOfReturning = DateTime.Now;

            BorrowCommand = new RelayCommandEmpty(Borrow);
        }

        private ObservableCollection<User> GetUsers()
        {
            return new ObservableCollection<User>(_uow.UserRepository.GetAll());
        }

        public DateTime SelectedDateOfReturning
        {
            get => _selectedDateOfReturning;
            set { _selectedDateOfReturning = value;OnPropertyChanged(nameof(SelectedDateOfReturning)); }

        }

        private void Borrow()
        {
            if(SelectedUser == null)
            {
                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Goldenrod,
                        Foreground = Brushes.White,
                        Title = "Наемане",
                        Message ="Моля изберете наемател",
                        Type = NotificationType.Warning
                    }, "WindowArea", TimeSpan.FromSeconds(3));
                return;
            }
            _uow.UserBookRepository.Add(new UserBooks()
            {
                BiblioId = Book.BiblioId,
                BookId = Book.BookId,
                LocationId = Book.LocationId,
                ShelfId = Book.ShelfId,
                UserId = SelectedUser.UserId,
                DateOfScheduledReturning = SelectedDateOfReturning,
                DateOfBorrowing = DateTime.Now,
                IsTaken = true
            });

            _uow.SaveChanges();

            ViewEventHandler.RaiseShowBookEvent(Book);

            _notificationManager.Show(
                new NotificationContent()
                {
                    Background = Brushes.Green,
                    Foreground = Brushes.White,
                    Title = "Наемане",
                    Message = "Книгата успешно наета",
                    Type = NotificationType.Success
                }, "WindowArea", TimeSpan.FromSeconds(3));
        }

        public Book Book
        {
            get => _book;
            set { _book = value; OnPropertyChanged(nameof(Book)); }
        }

        public ObservableCollection<User> UserList
        {
            get => _userList;
            set { _userList = value;OnPropertyChanged(nameof(UserList)); }
        }

        public User SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value;OnPropertyChanged(nameof(SelectedUser)); }
        }

        public bool IsFieldEnabled
        {
            get => _isFieldEnabled;
            set { _isFieldEnabled = value;OnPropertyChanged(nameof(IsFieldEnabled)); }
        }

        private void DisableFields()
        {
            IsFieldEnabled = false;
        }
    }
}
