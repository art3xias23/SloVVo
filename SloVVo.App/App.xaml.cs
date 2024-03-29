﻿using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using InfoBox;
using SloVVo.App.Spinners;
using SloVVo.App.Views;
using SloVVo.Logic.Squirrel;
using NLog;
using SloVVo.App.IoCKernel;
using SloVVo.Logic.ViewModels;
using MessageBox = System.Windows.MessageBox;

namespace SloVVo.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private SquirrelApplication squirrelApp;

        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public Task<(bool success, string message)> SquirrelUpdateInProgress;
        private MainWindowView _mainWidowView;

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            _logger.Debug("Starting App");
#if !DEBUG
            try
            {
                var squirrelUpdateLocation = ConfigurationManager.AppSettings["SquirrelUpdateLocation"];
                squirrelApp = new SquirrelApplication(squirrelUpdateLocation);
                SquirrelUpdateInProgress = squirrelApp.CheckForUpdates();
                SquirrelUpdateInProgress.ContinueWith(
                    (x =>
                    {
                        _logger.Trace("Checking for squirrel result");
                        var result = x.Result;
                        _logger.Trace("Got squirrel result");
                        HandleUpdate(result.success, result.message);
                    }));
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }



#endif
            IocKernel.Initialize(new IocConfiguration());


            InitialStart();
        }

        private void InitialStart()
        {
            var splash = new Splash();
            splash.Show();

            var booksViewModel = IocKernel.Get<BooksViewModel>();
            Task.Run(booksViewModel.BookUpdate);

            Task.Run(() =>
            Task.Delay(5000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    _mainWidowView = new MainWindowView();
                    _mainWidowView.Show();
                    splash.Close();
                });
            }));
        }


        private void HandleUpdate(bool success, string message)
        {
            _logger.Trace($"{nameof(squirrelApp.CheckForUpdates)} success: {success}, messsage: {message}");
            if (success)
                //MessageBox.Show("Нова версия на софтуера е налична! За да я използвате, моля рестартирайте SloVVo", "SloVVo", MessageBoxButton.OK, MessageBoxImage.Information);
                InformationBox.Show(
                    "Нова версия на Slovvo беше инсталирана, за да я използвате, моля рестартирайте Slovvo", "Ъпдейт",
                    "", "", "", InformationBoxButtons.OK, InformationBoxIcon.Success, "",
                    InformationBoxDefaultButton.Button1);
            else
                _logger.Warn(message);
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _logger.Trace("Disposing of Squirrel");
            squirrelApp?.Dispose();
        }
    }
}
