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
    public class BorrowViewModel : ObservableObject
    {
        private UnitOfWork _uow;

        private Book _book;
        private User _selectedUser;
        private DateTime _selectedDateOfReturning;
        private ObservableCollection<User> _userList;

        private bool _isFieldEnabled;

        public ICommand BorrowCommand { get; set; }
        public BorrowViewModel(Book book)
        {
            _uow = new UnitOfWork();
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
