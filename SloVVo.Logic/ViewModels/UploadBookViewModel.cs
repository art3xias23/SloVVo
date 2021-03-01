using System.Collections.ObjectModel;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;

namespace SloVVo.Logic.ViewModels
{
    public class UploadBookViewModel
    {
        private UnitOfWork _uow;
        public ICommand AddContentCommand { get; set; }
        public ICommand AddAuthorCommand { get; set; }
        public ICommand LoadWindowCommand { get; set; }
        public Book Book { get; set; }
        public ObservableCollection<Author> Authors { get; set; }
        public ObservableCollection<Section> Sections { get; set; }
        public UploadBookViewModel()
        {
            _uow = new UnitOfWork();
            Book = new Book();
            AddAuthorCommand = new RelayCommandEmpty(AddAuthor);
            AddContentCommand = new RelayCommandEmpty(AddContent);
            LoadWindowCommand = new RelayCommandEmpty(LoadWindow);
        }

        private void LoadWindow()
        {
            Authors = new ObservableCollection<Author>(_uow.AuthorRepository.GetAll());;
            Sections = new ObservableCollection<Section>(_uow.SectionRepository.GetAll());
        }

        private void AddContent()
        {
            Event.ViewEventHandler.RaiseShowAddContentView();
        }

        private void AddAuthor()
        {
            Event.ViewEventHandler.RaiseShowAddContentView();
        }
    }
}
