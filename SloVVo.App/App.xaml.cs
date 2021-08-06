using System;
using System.Threading.Tasks;
using System.Windows;
using SloVVo.App.Spinners;
using SloVVo.App.Views;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Event;
using System.Runtime.Caching;
using SloVVo.Logic.Caching;
using System.Configuration;
using System.Net;
using System.Windows.Navigation;
using SloVVo.Logic.Squirrel;
using NLog;
using MessageBox = System.Windows.MessageBox;

namespace SloVVo.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainWindow _mainWindow;
        private UploadBook _uploadBookWindow;
        private AddAuthorWindow _addAuthorWindow;
        private AddContentWindow _addContentWindow;
        private SquirrelApplication squirrelApp;

        private ILogger _logger = LogManager.GetCurrentClassLogger();

        public Task<(bool success, string message)> SquirrelUpdateInProgress;

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
                    //Sub(x)
                    //    Dim result As(success As Boolean, message As String) = x.Result
                    //    HandleUpdate(result.success, result.message)
                    //End Sub)
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
            _logger.Debug("Passes Squirrel Check Update");
            Task.Run(CacheBooksData);

            _logger.Debug("Cached Book Data");
            if (!ViewEventHandler.HasShowUploadScreenEventListeners)
            {
                ViewEventHandler.ShowUploadScreenEvent += ShowUploadScreen;
            }

            if (!ViewEventHandler.HasShowAddAuthorScreenEventListeners)
            {
                ViewEventHandler.ShowAddAuthorScreenEvent += ShowAddAuthorScreenEvent;
            }

            if (!ViewEventHandler.HasCloseAddAuthorScreenEventListeners)
            {
                ViewEventHandler.CloseAddAuthorScreenEvent += CloseAddAuthorScreen;
            }

            if (!ViewEventHandler.HasShowAddContentScreenEventListeners)
            {
                ViewEventHandler.ShowAddContentScreenEvent += ShowAddContentScreenEvent;
            }

            if (!ViewEventHandler.HasCloseAddContentScreenEventListeners)
            {
                ViewEventHandler.CloseAddContentScreenEvent += CloseAddContentScreen;
            }

            if (!ViewEventHandler.HasCloseUploadScreenEventListeners)
            {
                ViewEventHandler.CloseUploadScreenEvent += CloseUplaodScreen;
            }

            _logger.Debug("Opening splash screen");

            var splash = new Splash();
            splash.Show();
            try
            {
                Task.Delay(3000).ContinueWith(_ =>
                {
                    Dispatcher.Invoke(() =>
                    {
                        _mainWindow = new MainWindow();
                        _mainWindow.Show();
                        _logger.Trace("Closing SplashScreen");
                        splash.Close();
                    });
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void CacheBooksData()
        {
            try
            {
                _logger.Trace("Start Caching Books");
                var books = new UnitOfWork().BookRepository.GetAll();
                //var books = Task.Run(() =>  new UnitOfWork().BookRepository.GetAll());
                Cache.DefaultCache.Set("AllBooks", books,
                    new CacheItemPolicy() { AbsoluteExpiration = DateTimeOffset.MaxValue });
                _logger.Trace("Finish Caching Books");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        private void CloseUplaodScreen(object sender, EventArgs e)
        {
            _uploadBookWindow.Close();
        }

        private void CloseAddContentScreen(object sender, EventArgs e)
        {
            _addContentWindow.Close();
        }

        private void ShowAddContentScreenEvent(object sender, EventArgs e)
        {
            _addContentWindow = new AddContentWindow();
            _addContentWindow.Show();
        }

        private void ShowAddAuthorScreenEvent(object sender, EventArgs e)
        {
            _addAuthorWindow = new AddAuthorWindow();
            _addAuthorWindow.Show();
        }

        private void CloseAddAuthorScreen(object sender, EventArgs e)
        {
            _addAuthorWindow.Close();
        }

        private void ShowUploadScreen(object s, EventArgs e)
        {
            _uploadBookWindow = new UploadBook();
            _uploadBookWindow.Show();
        }

        private void HandleUpdate(bool success, string message)
        {
            _logger.Trace($"{nameof(squirrelApp.CheckForUpdates)} success: {success}, messsage: {message}");
            if (success)
                MessageBox.Show("Нова версия на софтуера е налична! За да я използвате, моля рестартирайте SloVVo", "SloVVo", MessageBoxButton.OK, MessageBoxImage.Information);
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
