using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SloVVo.ViewModels.Command;

namespace SloVVo.ViewModels.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        private readonly TextConverter _textConverter = new TextConverter(s => s.ToUpper());

        public enum SwitchViewEnum
        {
            View1 = 1,
            View2 = 2
        }

        public SwitchViewEnum _switchView;
        private string _someText;
        private readonly ObservableCollection<string> _history = new ObservableCollection<string>();

        public SwitchViewEnum SwitchView
        {
            get => _switchView;
            set => _switchView = value;
        }
        public string SomeText
        {
            get => _someText;
            set { _someText = value; OnPropertyChanged(nameof(SomeText));}
        }

        public IEnumerable<string> History => _history;

        public ICommand ConvertTextCommand => new RelayCommandEmpty(ConvertText);

        private void ConvertText()
        {
            if(string.IsNullOrWhiteSpace(SomeText))return;
            AddToHistory(_textConverter.ConvertText(SomeText));
            SomeText = string.Empty;
        }

        private void AddToHistory(string item)
        {
            if (!_history.Contains(item))
            {
                _history.Add(item);
            }
        }
    }
}
