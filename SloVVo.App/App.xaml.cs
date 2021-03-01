using System;
using System.Threading.Tasks;
using System.Windows;
using SloVVo.App.Spinners;
using SloVVo.App.Views;
using SloVVo.Logic.Event;

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

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
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

            var splash = new Splash();
            splash.Show();

            Task.Delay(3000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    _mainWindow = new MainWindow();
                    _mainWindow.Show();
                    splash.Close();
                });
            });
        }

        private void CloseAddContentScreen(object sender, EventArgs e)
        {
            _addContentWindow.Close();
            ViewEventHandler.CloseAddContentScreenEvent -= CloseAddContentScreen;
        }

        private void ShowAddContentScreenEvent(object sender, EventArgs e)
        {
            _addContentWindow = new AddContentWindow();
            _addContentWindow.Show();
            ViewEventHandler.ShowAddContentScreenEvent -= CloseAddContentScreen;
        }

        private void ShowAddAuthorScreenEvent(object sender, EventArgs e)
        {
            _addAuthorWindow = new AddAuthorWindow();
            _addAuthorWindow.Show();
            ViewEventHandler.ShowAddAuthorScreenEvent -= ShowAddAuthorScreenEvent;
        }

        private void CloseAddAuthorScreen(object sender, EventArgs e)
        {
            _addAuthorWindow.Close();
            ViewEventHandler.CloseAddAuthorScreenEvent -= CloseAddAuthorScreen;
        }

        private void ShowUploadScreen(object s, EventArgs e)
        {
            _uploadBookWindow = new UploadBook();
            _uploadBookWindow.Show();
            ViewEventHandler.ShowUploadScreenEvent -= ShowUploadScreen;
        }
    }
}
