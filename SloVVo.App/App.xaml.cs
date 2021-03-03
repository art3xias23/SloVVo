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

            if (!ViewEventHandler.HasCloseUploadScreenEventListeners)
            {
                ViewEventHandler.CloseUploadScreenEvent += CloseUplaodScreen;
            }

            var splash = new Splash();
            splash.Show();

            Task.Delay(1000).ContinueWith(_ =>
            {
                Dispatcher.Invoke(() =>
                {
                    _mainWindow = new MainWindow();
                    _mainWindow.Show();
                    splash.Close();
                });
            });
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
    }
}
