using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SloVVo.Models.Data;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
{
    public class SearchWindowViewModel : ObservableObject
    {
        public ICommand SearchUserControlLoadedCommand { get; set; }

        public ObservableCollection<Book> BooksList { get; set; }
        public SearchWindowViewModel()
        {
            SearchUserControlLoadedCommand = new RelayCommandEmpty(SearchUserControlLoaded); 
            BooksList = new ObservableCollection<Book>();
        }

        private void SearchUserControlLoaded()
        {
            BooksList.Add(new Book()
            {
                Author = "Todor",
                BibioId = 1,
                BookId = 1,
                BookName = "Some Book Name",
                Section = "Some Section",
                ShelfId = 1,
                YearOfPublication = 1994
            });
        }
    }
}
