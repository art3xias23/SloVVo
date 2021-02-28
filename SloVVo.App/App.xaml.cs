using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SloVVo.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ////var splashScreen = new Splash();
            ////this.MainWindow = splashScreen;
            ////splashScreen.Show();

            //Task.Factory.StartNew(() =>
            //{
            //    System.Threading.Thread.Sleep(3000);

            //    this.Dispatcher.Invoke(() =>
            //    {
            //        var mainWindow = new MainWindow();
            //        this.MainWindow = mainWindow;
            //        mainWindow.Show();
            //    });
            //});
        }
    }
}
