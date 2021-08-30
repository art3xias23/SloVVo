using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;

namespace SloVVo.Logic.ViewModels
{
    public class UserViewModel : ObservableObject
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

        public UserViewModel()
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
            _uow.UserRepository.Add(new User()
                {
                    Address = User.Address,
                    Firstname = User.Firstname,
                    Surname = User.Surname,
                    Location = User.Location,
                    TelephoneNumber = User.TelephoneNumber
            });

            _uow.SaveChanges();
        }
    }
}
