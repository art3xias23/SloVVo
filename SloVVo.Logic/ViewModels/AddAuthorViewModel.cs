using System;
using System.Windows.Input;
using SloVVo.Logic.Command;

namespace SloVVo.Logic.ViewModels
{
    public class AddAuthorViewModel : ObservableObject
    {
        public ICommand AddAuthorCommand { get; set; }

        public AddAuthorViewModel()
        {
           AddAuthorCommand = new RelayCommandEmpty(AddAuthor); 
        }

        private void AddAuthor()
        {
            throw new NotImplementedException();
        }
    }
}
