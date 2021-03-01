using System.Collections.ObjectModel;
using System.Windows.Input;
using SloVVo.Data.Models;
using SloVVo.Logic.Command;
using SloVVo.Logic.Event;

namespace SloVVo.Logic.ViewModels
{
    public class SearchWindowViewModel : ObservableObject
    {
        public ICommand SearchUserControlLoadedCommand { get; set; }
        public ICommand ShowUploadScreenCommand { get; set; }

        public ObservableCollection<Book> BooksList { get; set; }
        public SearchWindowViewModel()
        {
            SearchUserControlLoadedCommand = new RelayCommandEmpty(SearchUserControlLoaded); 
            ShowUploadScreenCommand = new RelayCommandEmpty(ShowUplaodScreen);
            BooksList = new ObservableCollection<Book>();
        }

        private void ShowUplaodScreen()
        {
            ViewEventHandler.RaiseShowUploadView();
        }

        private void SearchUserControlLoaded()
        {
            BooksList.Add(new Book()
            {
                AuthorId = 1,
                BibioId = 1,
                BookId = 1,
                BookName = "Some Book Name",
                SectionId = 2,
                ShelfId = 1,
                YearOfPublication = 1994
            });
        }
    }
}
