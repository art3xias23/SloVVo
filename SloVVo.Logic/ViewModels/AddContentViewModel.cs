using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class AddContentViewModel : ObservableObject
    {
        public ICommand AddContentCommand { get; set; }
        public string SectionName { get; set; }
        private readonly IUnitOfWork _uow;

        public AddContentViewModel(IUnitOfWork uow)
        {
            _uow = uow;
            AddContentCommand = new RelayCommandEmpty(AddContent);
        }

        private void AddContent()
        {
            AddContentItem();

            EventAggregator.UpdateSectionCollection();

            ViewEventHandler.RaiseShowAddBookView();
        }

        private void AddContentItem()
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
            }
        }
    }
}
