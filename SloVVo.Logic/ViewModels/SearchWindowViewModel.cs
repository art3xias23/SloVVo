using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Data.Repositories;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class SearchWindowViewModel : ObservableObject
    {
        private UnitOfWork _uow;

        public ICommand LoadBookCollectionCommand { get; set; }
        public ICommand ShowUploadScreenCommand { get; set; }

        public ObservableCollection<Book> BooksList { get; set; }
        public SearchWindowViewModel()
        {
            EventAggregator.BookUpdateTransmitted += BookUpdate;
            LoadBookCollectionCommand = new RelayCommandEmpty(LoadBookCollection); 
            ShowUploadScreenCommand = new RelayCommandEmpty(ShowUplaodScreen);
            BooksList = new ObservableCollection<Book>();

            _uow = new UnitOfWork();
        }

        private void BookUpdate()
        {
            LoadBookCollection();
        }

        private void ShowUplaodScreen()
        {
            ViewEventHandler.RaiseShowUploadView();
        }

        private void LoadBookCollection()
        {
            BooksList = new ObservableCollection<Book>(_uow.BookRepository.GetAll()); 
        }
    }
}
