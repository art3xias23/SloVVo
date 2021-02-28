using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
{
    public class MainWindowViewModel : ObservableObject
    {

        public MainWindowViewModel()
        {
            SwitchView = SwitchViewEnum.SearchWindow;
        }
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public enum SwitchViewEnum
        {
            SearchWindow = 1,
            View2 = 2
        }


        public SwitchViewEnum SwitchView { get; set; }
    }
}
