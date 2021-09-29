using System;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Notification.Wpf;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;
using SloVVo.Logic.Response;

namespace SloVVo.Logic.ViewModels
{
    public class AddAuthorViewModel : ObservableObject
    {
        private readonly IUnitOfWork _uow;
        private readonly NotificationManager _notificationManager;
        public ICommand AddAuthorCommand { get; set; }
        public string AuthorName { get; set; }

        public AddAuthorViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            _notificationManager = new NotificationManager();
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
        }

        private void AddAuthor()
        {
            var response = AddAuthorItem();

            if (response.Success)
            {
                EventAggregator.UpdateAuthorCollection();

                ViewEventHandler.RaiseShowAddBookView();
            }
        }

        private IResponse AddAuthorItem()
        {
            var recordExists = _uow.AuthorRepository.GetAll()
                .Any(x => x.AuthorName.ToLower() == AuthorName.ToLower());
            if (!recordExists)
            {
                _uow.AuthorRepository.Add(new Author()
                {
                    AuthorName = AuthorName
                });

                _uow.SaveChanges();

                _notificationManager.Show(new NotificationContent() { Background = Brushes.Green, Foreground = Brushes.White, Title = "Автор", Message = "Автор успешно добавен", Type = NotificationType.Success }, "WindowArea", TimeSpan.FromSeconds(3));
                return new Response.Response(true);
            }

            _notificationManager.Show(new NotificationContent() { Background = Brushes.Red, Foreground = Brushes.White, Title = "Автор", Message = "Авторът вече съществува", Type = NotificationType.Error }, "WindowArea", TimeSpan.FromSeconds(3));
            return new Response.Response(false);
        }
    }
}
