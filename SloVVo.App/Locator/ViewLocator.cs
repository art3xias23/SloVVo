using SloVVo.App.IoCKernel;
using SloVVo.App.Views;

namespace SloVVo.App.Locator
{
    public class ViewLocator
    {
        public static AddBookView AddBookView => IocKernel.Get<AddBookView>();
        public static BooksView BooksView => IocKernel.Get<BooksView>();
        public static AddUserView AddUserView => IocKernel.Get<AddUserView>();
        public static SearchUserView UsersView  => IocKernel.Get<SearchUserView>();
    }
}