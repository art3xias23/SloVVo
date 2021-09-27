using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class AddUserViewModel : ObservableObject
    {
        private UnitOfWork _uow;

        public ICommand LoadUserCommand { get; set; }
        public ICommand SaveUserCommand { get; set; }

        private User _user;

        public User User
        {
            get => _user;
            set { _user = value; OnPropertyChanged(nameof(User)); }
        }
        //public User User { get; set; }

        public AddUserViewModel()
        {
            _uow = new UnitOfWork();
            User = new User();
            LoadUserCommand = new RelayCommand(LoadUser);
            SaveUserCommand = new RelayCommand(SaveUser);
        }

        private void LoadUser(object obj)
        {
        }

        public void SaveUser(object obj)
        {
            AddUser();
            EventAggregator.UpdateUserCollection();
            ViewEventHandler.RaiseShowUsersEvent();
        }

        private void AddUser()
        {
            if (_uow.UserRepository.Any(x => x.Firstname == User.Firstname && x.Surname == User.Surname)) return;
            _uow.UserRepository.Add(new User()
            {
                Address = User.Address,
                Firstname = User.Firstname,
                Surname = User.Surname,
                Town = User.Town,
                TelephoneNumber = User.TelephoneNumber
            });

            _uow.SaveChanges();
        }
    }
}
