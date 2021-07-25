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

        private User _user;
        public User User
        {
            get { return _user;}
            set { _user = value; OnPropertyChanged(nameof(User)); }
        }

        public UserViewModel()
        {
                LoadUserCommand = new RelayCommand(LoadUser);
        }

        private void LoadUser(object obj)
        {
            User = new User();
        }
    }
}
