using SloVVo.App.IoCKernel;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.Locator
{
    public class ViewModelLocator
    {
        public MainWindowViewModel.MainWindowViewModel MainWindowViewModel => IocKernel.Get<MainWindowViewModel.MainWindowViewModel>();


        public AddBookViewModel AddBookViewModel => IocKernel.Get<AddBookViewModel>();

        public BooksViewModel BooksViewModel => IocKernel.Get<BooksViewModel>();
        public AddUserViewModel AddUserViewModel => IocKernel.Get<AddUserViewModel>();
        public UsersViewModel SearchUserViewModel => IocKernel.Get<UsersViewModel>();
        public BookViewModel BookViewModel => IocKernel.Get<BookViewModel>();
    }
}