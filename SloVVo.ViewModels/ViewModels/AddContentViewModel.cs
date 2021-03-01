using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
{
    public class AddContentViewModel : ObservableObject
    {
        public ICommand AddContentCommand { get; set; }
        public AddContentViewModel()
        {
            AddContentCommand = new RelayCommandEmpty(AddContent);
        }

        private void AddContent()
        {
            Event.ViewEventHandler.RaiseShowUploadView();
        }
    }
}
