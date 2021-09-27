using Ninject.Modules;
using SloVVo.Data.Models;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.IoCKernel
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<MainWindowViewModel.MainWindowViewModel>().ToSelf().InTransientScope();
            Bind<AddAuthorViewModel>().ToSelf().InTransientScope();
            Bind<AddBookViewModel>().ToSelf().InSingletonScope();
            Bind<AddContentViewModel>().ToSelf().InTransientScope();
            Bind<UsersViewModel>().ToSelf().InTransientScope();
            Bind<AddUserViewModel>().ToSelf().InTransientScope();
            Bind<BookViewModel>().ToSelf().WithConstructorArgument("book");
        }
    }
}