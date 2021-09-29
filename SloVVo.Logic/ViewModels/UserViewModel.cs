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
    public class UserViewModel : ObservableObject
    {

        public ICommand EditUserCommand { get; set; }
        private readonly IUnitOfWork _uow;
        private readonly NotificationManager _notificationManager;

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

        public UserViewModel(User user, IUnitOfWork uow)
        {
            _notificationManager = new NotificationManager();
            _uow = uow;
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
            UserBooks = new ObservableCollection<UserBooks>(_uow.UserBookRepository.GetAll().Where(x =>
               x.UserId == User.UserId).ToList());
        }

        private void EditUser(object obj)
        {
            if (EditButtonContent == "Запази")
            {
                UpdateRecord();
                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Green,
                        Foreground = Brushes.White,
                        Title = "Потребител",
                        Message = "Потребителят успешно променен",
                        Type = NotificationType.Success
                    }, "WindowArea", TimeSpan.FromSeconds(3));
            }
            SetEditButtonContent();
        }

        private void UpdateRecord()
        {
            _uow.UserRepository.Update(User);
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
