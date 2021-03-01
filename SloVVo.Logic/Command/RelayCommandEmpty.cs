using System;
using System.Windows.Input;

namespace SloVVo.Logic.Command
{
    class RelayCommandEmpty : ICommand
    {
        private readonly Action _execute;
        private readonly Predicate<object> _canExecute;

        public RelayCommandEmpty(Action execute) : this(execute, null) { }

        public RelayCommandEmpty(Action execute, Predicate<object> canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }
        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke(parameter) ?? true;
        }


        public void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}

