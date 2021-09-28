using Ninject.Modules;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.IoCKernel
{
    class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
            Bind<MainWindowViewModel.MainWindowViewModel>().ToSelf().InTransientScope();

            Bind<AddAuthorViewModel>().ToSelf().InTransientScope();
            Bind<AddBookViewModel>().ToSelf().InSingletonScope();
            Bind<AddContentViewModel>().ToSelf().InTransientScope();
            Bind<AddUserViewModel>().ToSelf().InTransientScope();
            Bind<UsersViewModel>().ToSelf().InTransientScope();
            Bind<BooksViewModel>().ToSelf().InTransientScope();
        }
    }
}