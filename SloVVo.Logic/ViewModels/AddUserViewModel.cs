using System;
using System.Windows.Input;
using System.Windows.Media;
using Notification.Wpf;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;
using SloVVo.Logic.Response;

namespace SloVVo.Logic.ViewModels
{
    public class AddUserViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;
        private readonly NotificationManager _notificationManager;

        public ICommand SaveUserCommand { get; set; }

        private User _user;

        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public AddUserViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            User = new User();
            _notificationManager = new NotificationManager();
            SaveUserCommand = new RelayCommand(SaveUser);
        }

        public void SaveUser(object obj)
        {
            AddUser();
            EventAggregator.UpdateUserCollection();
            ViewEventHandler.RaiseShowUsersEvent();
        }

        private IResponse AddUser()
        {
            var userExists = _uow.UserRepository.Any(x => x.Firstname == User.Firstname && x.Surname == User.Surname);
            if (!userExists)
            {

                _uow.UserRepository.Add(new User()
                {
                    Address = User.Address,
                    Firstname = User.Firstname,
                    Surname = User.Surname,
                    Town = User.Town,
                    TelephoneNumber = User.TelephoneNumber
                });

                _uow.SaveChanges();
                _notificationManager.Show(
                    new NotificationContent()
                    {
                        Background = Brushes.Green, Foreground = Brushes.White, Title = "Потребител",
                        Message = "Потребителят успешно добавена", Type = NotificationType.Success
                    }, "WindowArea", TimeSpan.FromSeconds(3));
                return new Response.Response(true);
            }

            _notificationManager.Show(
                new NotificationContent()
                {
                    Background = Brushes.Red, Foreground = Brushes.White, Title = "Потребител",
                    Message = "Потребителят вече съществува", Type = NotificationType.Error
                }, "WindowArea", TimeSpan.FromSeconds(3));
            return new Response.Response(false);
        }
    }
}
