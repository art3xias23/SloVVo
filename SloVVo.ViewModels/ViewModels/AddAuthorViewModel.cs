using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
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
