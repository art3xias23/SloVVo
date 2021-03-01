using System.Windows.Input;
using SloVVo.Logic.Command;

namespace SloVVo.Logic.ViewModels
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
