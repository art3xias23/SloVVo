using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using NLog;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class UsersViewModel : ObservableObject
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private UnitOfWork UnitOfWork;
        private User _selectedUser;

        private ObservableCollection<User> _UsersList;
        public ObservableCollection<User> UsersList
        {
            get => _UsersList;
            set { _UsersList = value; OnPropertyChanged(nameof(UsersList)); }
        }

        public ICommand LoadUserCollectionCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand EditRowCommand { get; set; }

        public User SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(nameof(SelectedUser)); }
        }

        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private bool _isFirstNameChecked;

        public bool IsFirstNameChecked
        {
            get => _isFirstNameChecked;
            set { _isFirstNameChecked = value; OnPropertyChanged(nameof(IsFirstNameChecked)); }
        }

        private bool _isSurnameChecked;

        public bool IsSurnameChecked
        {
            get => _isSurnameChecked;
            set { _isSurnameChecked = value; OnPropertyChanged(nameof(IsSurnameChecked)); }
        }

        private Visibility _isUsersVisible;

        public Visibility IsUsersVisible
        {
            get => _isUsersVisible;
            set { _isUsersVisible = value; OnPropertyChanged(nameof(IsUsersVisible)); }
        }

        private int _totalItemsCount;

        public int TotalItemsCount
        {
            get => _totalItemsCount;
            set
            {
                _totalItemsCount = value; OnPropertyChanged(nameof(TotalItemsCount));
            }
        }

        private string _totalItemsText;
        public string TotalItemsText
        {
            get => _totalItemsText;
            set
            {
                _totalItemsText = value; OnPropertyChanged(nameof(TotalItemsText));
            }
        }

        public UsersViewModel()
        {
            EventAggregator.UserUpdateTransmitted += UserUpdate;

            LoadUserCollectionCommand = new RelayCommandEmpty(LoadUserCollection);
            SearchCommand = new RelayCommand(Search);
            EditRowCommand = new RelayCommandEmpty(EditRow);

            IsUsersVisible = Visibility.Visible;
            IsUsersVisible = Visibility.Hidden;
        }

        private void EditRow()
        {
            if (SelectedUser != null)
                ViewEventHandler.RaiseShowUserEvent(SelectedUser);
        }

        private void UserUpdate()
        {
            LoadUsersCollection();
        }

        private void Search(object obj)
        {
            UsersList = new ObservableCollection<User>(GetUsersList());
        }

        private List<User> GetUsersList()
        {
            if (!string.IsNullOrWhiteSpace(SearchText))
            {

                if (IsFirstNameChecked && !IsSurnameChecked)
                {
                    var Users = UnitOfWork.UserRepository.GetAll();

                    return Users.Where(x => x.Firstname != null && x.Firstname.ToLower().Contains(SearchText.ToLower())).ToList();
                }
                else if (IsSurnameChecked && !IsFirstNameChecked)
                {
                    var Users = UnitOfWork.UserRepository.GetAll();
                    return Users.Where(x => x.Surname != null && x.Surname.ToLower().Contains(SearchText.ToLower()))?.ToList();
                }
                else
                {
                    var Users = UnitOfWork.UserRepository.GetAll();
                    return Users.ToList();
                }

            }

            return UnitOfWork.UserRepository.GetAll().ToList();
        }

        private void LoadUserCollection()
        {
            _logger.Trace("Loading Users");
            UsersList = new ObservableCollection<User>(UnitOfWork.UserRepository.GetAllQueryable().ToList());
            SetTotalItems("Брой Потребители", UsersList.Count);
        }


        private void LoadUsersCollection()
        {
            _logger.Trace("Loading Users");
            UsersList = new ObservableCollection<User>(UnitOfWork.UserRepository.GetAllQueryable());
        }

        private void SetTotalItems(string text, int count)
        {
            TotalItemsText = text;
            TotalItemsCount = count;
        }
    }
}
