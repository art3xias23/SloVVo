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
    public class AddContentViewModel : ObservableObject
    {
        public ICommand AddContentCommand { get; set; }
        public string SectionName { get; set; }
        private readonly IUnitOfWork _uow;
        private readonly NotificationManager _notificationManager;

        public AddContentViewModel(IUnitOfWork uow)
        {
            _notificationManager = new NotificationManager();
            _uow = uow;
            AddContentCommand = new RelayCommandEmpty(AddContent);
        }

        private void AddContent()
        {
            if (AddContentItem().Success)
            {
                EventAggregator.UpdateSectionCollection();

                ViewEventHandler.RaiseShowAddBookView();
            }
        }

        private IResponse AddContentItem()
        {
            var recordExists = _uow.SectionRepository.GetAll()
                .Any(x => x.SectionName.ToLower() == SectionName.ToLower());
            if (!recordExists)
            {
                _uow.SectionRepository.Add(new Section()
                {
                    SectionName = SectionName
                });

                _uow.SaveChanges();

                _notificationManager.Show(new NotificationContent() { Background = Brushes.Green, Foreground = Brushes.White, Title = "Раздел", Message = "Раздел успешно добавен", Type = NotificationType.Success }, "WindowArea", TimeSpan.FromSeconds(3));
                return new Response.Response(true);
            }
            _notificationManager.Show(new NotificationContent() { Background = Brushes.Red, Foreground = Brushes.White, Title = "Раздел", Message = "Разделът вече съществува", Type = NotificationType.Error }, "WindowArea", TimeSpan.FromSeconds(3));
            return new Response.Response(false);
        }
    }
}
