﻿using Ninject.Modules;
using Notification.Wpf;
using Notifications.Wpf;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.PdfLogic;
using SloVVo.Logic.ViewModels;

namespace SloVVo.App.IoCKernel
{
    class IocConfiguration : NinjectModule 
    {
        public override void Load() 
        {
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();
            Bind<MainWindowViewModel.MainWindowViewModel>().ToSelf().InTransientScope();

            Bind<AddBookViewModel>().ToSelf().InTransientScope();
            Bind<AddUserViewModel>().ToSelf().InTransientScope();
            Bind<BooksViewModel>().ToSelf().InTransientScope();
        }
    }
}