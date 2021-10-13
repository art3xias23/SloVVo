using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using SloVVo.App.Views;
using SloVVo.Data.EventArgs;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;
using SloVVo.Logic.ViewModels;
using ViewLocator = SloVVo.App.Locator.ViewLocator;

namespace SloVVo.App.MainWindowViewModel
{
    public class MainWindowViewModel : ObservableObject
    {
        public ICommand ShowAddBookCommand { get; set; }
        public ICommand ShowAddUserCommand { get; set; }
        public ICommand ShowBooksCommand { get; set; }
        public ICommand ShowUsersCommand { get; set; }

        public ICommand LogoutCommand { get; set; }
        public ICommand CloseMenuCommand { get; set; }
        public ICommand OpenMenuCommand { get; set; }

        private Visibility _buttonOpenMenuVisibility;
        private Visibility _buttonCloseMenuVisibility;
        private readonly IUnitOfWork _uow;

        public Visibility ButtonOpenMenuVisibility
        {
            get => _buttonOpenMenuVisibility;
            set { _buttonOpenMenuVisibility = value; OnPropertyChanged(nameof(ButtonOpenMenuVisibility)); }
        }

        public Visibility ButtonCloseMenuVisibility
        {
            get => _buttonCloseMenuVisibility;
            set { _buttonCloseMenuVisibility = value; OnPropertyChanged(nameof(ButtonCloseMenuVisibility)); }
        }

        public MainWindowViewModel()
        {
            ShowBooks();
            _uow = IoCKernel.IocKernel.Get<IUnitOfWork>();
            LogoutCommand = new RelayCommandEmpty(Logout);
            CloseMenuCommand = new RelayCommandEmpty(CloseMenu);
            OpenMenuCommand = new RelayCommandEmpty(OpenMenu);

            ShowAddBookCommand = new RelayCommandEmpty(ShowAddBookScreen);
            ShowAddUserCommand = new RelayCommandEmpty(ShowAddUser);
            ShowBooksCommand = new RelayCommandEmpty(ShowBooks);
            ShowUsersCommand = new RelayCommandEmpty(ShowUsers);
            SlovvoNameAndVersion =
                $"Slovvo - {System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0,5)}";

            ButtonCloseMenuVisibility = Visibility.Collapsed;
            ButtonOpenMenuVisibility = Visibility.Visible;

            ViewEventHandler.ShowAddBookScreenAction = ShowAddBookActionMethod;
            ViewEventHandler.ShowBooksEvent += ShowBooksEventMethod;
            ViewEventHandler.ShowUsersEvent += ShowUsersEventMethod;
            ViewEventHandler.ShowBookEvent += ShowBookEventMethod;
            ViewEventHandler.ShowBorrowEvent += ShowBorrowEventMethod;
            ViewEventHandler.ShowUserEvent += ShowUserEventMethod;
        }

        private void ShowUserEventMethod(object sender, UserEventArgs e)
        {
            var viewModel = new UserViewModel(e.User, _uow);
            PageContent = new UserView(viewModel);
        }

        private void ShowBorrowEventMethod(object sender, BookEventArgs e)
        {
            var viewModel = new BorrowViewModel(e.Book, _uow);
            PageContent = new BorrowView(viewModel);
        }

        private void ShowBookEventMethod(object sender, BookEventArgs e)
        {
            var viewModel = new BookViewModel(e.Book, _uow);
            PageContent = new BookView(viewModel);
        }

        private void ShowUsersEventMethod(object sender, EventArgs e)
        {
            ShowUsers();
        }

        private void ShowBooksEventMethod(object sender, EventArgs e)
        {
            ShowBooks();
        }

        private void ShowAddBookActionMethod()
        {
            ShowAddBookScreen();
        }

        private void ShowBooks()
        {
            PageContent = ViewLocator.BooksView;
        }
        private void ShowUsers()
        {
            PageContent = ViewLocator.UsersView;
        }

        private void ShowAddUser()
        {
            PageContent = ViewLocator.AddUserView;
        }

        private void ShowAddBookScreen()
        {
            PageContent = ViewLocator.AddBookView;
        }

        private void OpenMenu()
        {
            ButtonCloseMenuVisibility = Visibility.Visible;
            ButtonOpenMenuVisibility = Visibility.Collapsed;
        }

        private void CloseMenu()
        {
            ButtonCloseMenuVisibility = Visibility.Collapsed;
            ButtonOpenMenuVisibility = Visibility.Visible;
        }

        private void Logout()
        {
            Application.Current.Shutdown();
        }

        private object _pageContent;
        public object PageContent
        {
            get => _pageContent;
            set { _pageContent = value; OnPropertyChanged(nameof(PageContent)); }
        }

        private string _slovvoNameAndVersion;

        public string SlovvoNameAndVersion
        {
            get => _slovvoNameAndVersion;
            set { _slovvoNameAndVersion = value;OnPropertyChanged(nameof(SlovvoNameAndVersion)); }
        }
    }
}
