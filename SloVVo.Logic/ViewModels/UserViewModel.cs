using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class UserViewModel : ObservableObject
    {
        private UnitOfWork UnitOfWork;

        public ICommand LoadUserCommand { get; set; }
        public ICommand EditUserCommand { get; set; }

        private User _user;
        private string _editButtonContent;
        private bool _isFieldEnabled;
        private ObservableCollection<UserBooks> _userBooks;
        public User CurrentUser { get; set; }
        public ObservableCollection<UserBooks> UserBooks { 
            get => _userBooks;
            set
            {
                _userBooks = value;
                OnPropertyChanged(nameof(UserBooks));
            }
        }

        public bool IsFieldEnabled
        {
            get => _isFieldEnabled;
            set { _isFieldEnabled = value; OnPropertyChanged(nameof(IsFieldEnabled)); }
        }

        public string EditButtonContent
        {
            get => _editButtonContent;
            set { _editButtonContent = value; OnPropertyChanged(nameof(EditButtonContent)); }
        }

        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(nameof(User)); }
        }

        public UserViewModel(User user)
        {
            User = user;
            CurrentUser = new User();
            if (user != null)
                CurrentUser.UserId = user.UserId;
            EditUserCommand = new RelayCommand(EditUser);
            SetEditButtonContent();
            GetUserBooks();
        }

        private void GetUserBooks()
        {
            UserBooks = new ObservableCollection<UserBooks>(UnitOfWork.UserBookRepository.GetAll().Where(x =>
               x.UserId == User.UserId).ToList());
        }

        private void EditUser(object obj)
        {
            if (EditButtonContent == "Запази")
            {
                UpdateRecord();
            }
            SetEditButtonContent();
        }

        private void UpdateRecord()
        {
            UnitOfWork.UserRepository.Update(User);
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
    }
}
