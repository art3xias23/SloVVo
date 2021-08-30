using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using NLog;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class SearchWindowViewModel : ObservableObject
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
        private UnitOfWork _uow;

        private ObservableCollection<Book> _booksList;
        public ObservableCollection<Book> BooksList
        {
            get => _booksList;
            set { _booksList = value; OnPropertyChanged(nameof(BooksList)); }
        }

        public ICommand LoadBookCollectionCommand { get; set; }
        public ICommand LoadUserCollectionCommand { get; set; }
        public ICommand ShowUploadScreenCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand BooksClickCommand { get; set; }
        public ICommand UsersClickCommand { get; set; }
        public ICommand AddUserClickCommand { get; set; }

        private ObservableCollection<User> _usersList;
        public ObservableCollection<User> UsersList
        {
            get => _usersList;
            set { _usersList = value; OnPropertyChanged(nameof(UsersList)); }
        }


        private string _searchText;
        public string SearchText
        {
            get => _searchText;
            set { _searchText = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private bool _isBookChecked;

        public bool IsBookChecked
        {
            get => _isBookChecked;
            set { _isBookChecked = value; OnPropertyChanged(nameof(IsBookChecked)); }
        }

        private bool _isAuthorChecked;

        public bool IsAuthorChecked
        {
            get => _isAuthorChecked;
            set { _isAuthorChecked = value; OnPropertyChanged(nameof(IsAuthorChecked)); }
        }

        private Visibility _isBooksVisible;
        public Visibility IsBooksVisible
        {
            get => _isBooksVisible;
            set { _isBooksVisible = value; OnPropertyChanged(nameof(IsBooksVisible)); }
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


        public SearchWindowViewModel()
        {
            EventAggregator.BookUpdateTransmitted += BookUpdate;

            LoadBookCollectionCommand = new RelayCommandEmpty(LoadBookCollection);
            LoadUserCollectionCommand = new RelayCommand(LoadUsersCollection);
            ShowUploadScreenCommand = new RelayCommandEmpty(ShowUplaodScreen);
            SearchCommand = new RelayCommand(Search);
            BooksClickCommand = new RelayCommand(ClickBooks);
            UsersClickCommand = new RelayCommand(ClickUsers);
            AddUserClickCommand = new RelayCommand(AddUser);

            IsBooksVisible = Visibility.Visible;
            IsUsersVisible = Visibility.Hidden;

            _uow = new UnitOfWork();
        }

        private void ClickBooks(object obj=null)
        {
            IsBooksVisible = Visibility.Visible;
            IsUsersVisible = Visibility.Hidden;
            SetTotalItems("Брой Книги", BooksList.Count);

        }

        private void ClickUsers(object obj)
        {
            IsBooksVisible = Visibility.Hidden;
            IsUsersVisible = Visibility.Visible;
            SetTotalItems("Брой Потребители", UsersList.Count);
        }
        private void AddUser(object obj)
        {
            ViewEventHandler.RaiseShowAddUserEvent();
        }

        private void Search(object obj)
        {
            BooksList = new ObservableCollection<Book>(GetBooksList());
        }

        private List<Book> GetBooksList()
        {
            var booksList = new List<Book>();

            if (IsBookChecked)
            {
                return booksList = _uow.BookRepository.GetAll()
                    .Where(x => x.BookName.ToLower().Contains(SearchText.ToLower())).ToList();
            }
            else if (IsAuthorChecked)
            {
                return booksList = _uow.BookRepository.GetAll()
                    .Where(x => x.Author.AuthorName.ToLower().Contains(SearchText.ToLower())).ToList();
            }

            return booksList;
        }

        private void BookUpdate()
        {
            LoadBookCollection();
        }

        private void ShowUplaodScreen()
        {
            ViewEventHandler.RaiseShowUploadView();
        }

        private void LoadBookCollection()
        {
            _logger.Trace("Loading Books");
            BooksList = new ObservableCollection<Book>(_uow.BookRepository.GetAllQueryable());
            SetTotalItems("Брой Книги", BooksList.Count);
        }


        private void LoadUsersCollection(object obj)
        {
            _logger.Trace("Loading Users");
            UsersList = new ObservableCollection<User>(_uow.UserRepository.GetAllQueryable());
        }

        private void SetTotalItems(string text, int count)
        {
            TotalItemsText = text;
            TotalItemsCount = count;
        }
    }
}
